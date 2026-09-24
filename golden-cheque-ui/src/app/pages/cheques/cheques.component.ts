import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePipe, DecimalPipe } from '@angular/common';
import { forkJoin } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

import { ChequeApi, FactorApi, LookupsService } from '../../core/api.service';
import { Cheque, Customer, Factor, Shobe } from '../../core/models';

const CHEQUE_STATUS: Record<number, { label: string; cls: string }> = {
  0: { label: 'در جریان', cls: 'badge-gray' },
  1: { label: 'پاس شده', cls: 'badge-green' },
  2: { label: 'برگشتی', cls: 'badge-red' },
};

@Component({
  selector: 'app-cheques',
  standalone: true,
  imports: [ReactiveFormsModule, DatePipe, DecimalPipe],
  template: `
  <div class="page-head">
    <div>
      <h2>چک‌ها</h2>
      <div class="sub">ثبت و پیگیری چک‌های دریافتی</div>
    </div>
    <button class="btn btn-primary" (click)="openNew()">+ چک جدید</button>
  </div>

  @if (error()) { <div class="alert alert-error">{{ error() }}</div> }

  <div class="table-wrap">
    <table>
      <thead>
        <tr>
          <th>شماره چک</th><th>شماره حساب</th><th>مبلغ</th><th>صادرکننده</th>
          <th>شعبه</th><th>سررسید</th><th>وضعیت</th><th></th>
        </tr>
      </thead>
      <tbody>
        @for (c of list(); track c.id) {
          <tr>
            <td>{{ c.shomareChek }}</td>
            <td dir="ltr">{{ c.shomareHesab }}</td>
            <td>{{ c.chequePrice | number }}</td>
            <td>{{ customerName(c.sahabCheque) }}</td>
            <td>{{ shobeName(c.shobe) }}</td>
            <td>{{ c.passDate | date:'yyyy/MM/dd' }}</td>
            <td><span class="badge {{ status(c.chequeStatus).cls }}">{{ status(c.chequeStatus).label }}</span></td>
            <td class="actions">
              <button class="btn btn-ghost btn-sm" (click)="openEdit(c)">ویرایش</button>
              <button class="btn btn-danger btn-sm" (click)="remove(c)">حذف</button>
            </td>
          </tr>
        } @empty {
          <tr><td colspan="8"><div class="empty"><div class="icon">🧾</div>چکی ثبت نشده است</div></td></tr>
        }
      </tbody>
    </table>
  </div>

  @if (editing()) {
    <div class="modal-backdrop" (click)="close()">
      <div class="modal card" (click)="$event.stopPropagation()">
        <h3>{{ editing()!.id ? 'ویرایش چک' : 'چک جدید' }}</h3>
        @if (formError()) { <div class="alert alert-error">{{ formError() }}</div> }

        <form [formGroup]="form" (ngSubmit)="save()">
          <div class="grid-2">
            <div class="field"><label>شماره چک *</label><input type="number" formControlName="shomareChek"></div>
            <div class="field"><label>شماره حساب *</label><input type="number" formControlName="shomareHesab"></div>
            <div class="field"><label>مبلغ (تومان) *</label><input type="number" formControlName="chequePrice"></div>
            <div class="field"><label>صادرکننده *</label>
              <select formControlName="sahabCheque">
                <option value="">انتخاب کنید…</option>
                @for (cu of customers(); track cu.id) { <option [value]="cu.id">{{ cu.name }} {{ cu.lastName }}</option> }
              </select>
            </div>
            <div class="field"><label>شعبه بانک *</label>
              <select formControlName="shobe">
                <option value="">انتخاب کنید…</option>
                @for (s of shobes(); track s.id) { <option [value]="s.id">{{ s.name }}</option> }
              </select>
            </div>
            <div class="field"><label>فاکتور مرتبط</label>
              <select formControlName="factorID">
                <option value="">بدون فاکتور</option>
                @for (f of factors(); track f.id) { <option [value]="f.id">فاکتور {{ f.id.slice(0,8) }}</option> }
              </select>
            </div>
            <div class="field"><label>تاریخ چک *</label><input type="date" formControlName="chequeDate"></div>
            <div class="field"><label>سررسید *</label><input type="date" formControlName="passDate"></div>
            <div class="field"><label>وضعیت *</label>
              <select formControlName="chequeStatus">
                <option [ngValue]="0">در جریان</option>
                <option [ngValue]="1">پاس شده</option>
                <option [ngValue]="2">برگشتی</option>
              </select>
            </div>
            <div class="field"><label>توضیحات</label><input formControlName="detail"></div>
          </div>

          <div class="modal-actions">
            <button type="button" class="btn btn-ghost" (click)="close()">انصراف</button>
            <button type="submit" class="btn btn-primary" [disabled]="saving()">
              {{ saving() ? 'در حال ذخیره…' : 'ذخیره' }}
            </button>
          </div>
        </form>
      </div>
    </div>
  }
  `,
  styles: [`
    .actions { display: flex; gap: 6px; }
    .modal-backdrop { position: fixed; inset: 0; background: rgba(15,15,20,.5); display: grid; place-items: center; z-index: 100; padding: 16px; }
    .modal { width: 100%; max-width: 640px; max-height: 90vh; overflow-y: auto; }
    .modal h3 { margin-bottom: 16px; }
    .modal-actions { display: flex; justify-content: flex-end; gap: 8px; margin-top: 8px; }
  `],
})
export class ChequesComponent implements OnInit {
  private api = inject(ChequeApi);
  private lookups = inject(LookupsService);
  private factorApi = inject(FactorApi);
  private fb = inject(FormBuilder);

  list = signal<Cheque[]>([]);
  customers = signal<Customer[]>([]);
  shobes = signal<Shobe[]>([]);
  factors = signal<Factor[]>([]);
  error = signal<string | null>(null);
  formError = signal<string | null>(null);
  editing = signal<Cheque | null>(null);
  saving = signal(false);

  form = this.fb.nonNullable.group({
    shomareChek: [0, Validators.required],
    shomareHesab: [0, Validators.required],
    chequePrice: [0, Validators.required],
    sahabCheque: ['', Validators.required],
    shobe: ['', Validators.required],
    factorID: [''],
    chequeDate: ['', Validators.required],
    passDate: ['', Validators.required],
    chequeStatus: [0, Validators.required],
    detail: [''],
  });

  customerName(id: string) { const c = this.customers().find(x => x.id === id); return c ? `${c.name} ${c.lastName}` : '—'; }
  shobeName(id: string) { return this.shobes().find(s => s.id === id)?.name ?? '—'; }
  status(s: number) { return CHEQUE_STATUS[s] ?? CHEQUE_STATUS[0]; }

  ngOnInit(): void {
    this.reload();
  }

  openNew(): void {
    this.formError.set(null);
    this.form.reset({
      shomareChek: 0, shomareHesab: 0, chequePrice: 0,
      sahabCheque: '', shobe: '', factorID: '',
      chequeDate: new Date().toISOString().slice(0, 10),
      passDate: new Date().toISOString().slice(0, 10),
      chequeStatus: 0, detail: '',
    });
    this.editing.set({ id: '' } as Cheque);
  }

  openEdit(c: Cheque): void {
    this.formError.set(null);
    this.form.patchValue({
      shomareChek: c.shomareChek, shomareHesab: c.shomareHesab, chequePrice: c.chequePrice,
      sahabCheque: c.sahabCheque, shobe: c.shobe,
      factorID: c.factorID || '', chequeDate: c.chequeDate?.slice(0, 10), passDate: c.passDate?.slice(0, 10),
      chequeStatus: c.chequeStatus, detail: c.detail ?? '',
    });
    this.editing.set(c);
  }

  close(): void { this.editing.set(null); }

  save(): void {
    if (this.form.invalid) return;
    this.saving.set(true);
    this.formError.set(null);
    const v = this.form.getRawValue();
    const payload = {
      kind: 0,
      shomareChek: Number(v.shomareChek), shomareHesab: Number(v.shomareHesab),
      chequePrice: Number(v.chequePrice),
      sahabCheque: v.sahabCheque, shobe: v.shobe,
      factorID: v.factorID || '00000000-0000-0000-0000-000000000000',
      chequeDate: new Date(v.chequeDate).toISOString(),
      passDate: new Date(v.passDate).toISOString(),
      chequeStatus: Number(v.chequeStatus),
      detail: v.detail, visable: true,
    };
    const current = this.editing()!;
    const req = current.id ? this.api.update(current.id, payload) : this.api.create(payload);
    req.subscribe({
      next: () => { this.saving.set(false); this.close(); this.reload(); },
      error: (err: HttpErrorResponse) => {
        this.saving.set(false);
        this.formError.set(err.error?.toString() || 'ذخیره ناموفق بود');
      },
    });
  }

  remove(c: Cheque): void {
    if (!confirm(`حذف چک شماره ${c.shomareChek}؟`)) return;
    this.api.delete(c.id).subscribe({ next: () => this.reload() });
  }

  private reload(): void {
    forkJoin({
      cheques: this.api.getAll(),
      customers: this.lookups.customers(),
      shobes: this.lookups.shobes(),
      factors: this.factorApi.getAll(),
    }).subscribe(({ cheques, customers, shobes, factors }) => {
      this.list.set(cheques ?? []);
      this.customers.set(customers ?? []);
      this.shobes.set(shobes ?? []);
      this.factors.set(factors ?? []);
    });
  }
}
