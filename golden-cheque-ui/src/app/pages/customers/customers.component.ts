import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePipe, DecimalPipe } from '@angular/common';
import { forkJoin } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

import { CustomerApi, LookupsService } from '../../core/api.service';
import { City, Customer, CustomerRate } from '../../core/models';

@Component({
  selector: 'app-customers',
  standalone: true,
  imports: [ReactiveFormsModule, DatePipe, DecimalPipe],
  template: `
  <div class="page-head">
    <div>
      <h2>مشتریان</h2>
      <div class="sub">مدیریت پرونده مشتریان و رتبه آن‌ها</div>
    </div>
    <button class="btn btn-primary" (click)="openNew()">+ مشتری جدید</button>
  </div>

  @if (error()) { <div class="alert alert-error">{{ error() }}</div> }

  <div class="table-wrap">
    <table>
      <thead>
        <tr>
          <th>کد</th><th>نام و نام خانوادگی</th><th>موبایل</th><th>شهر</th>
          <th>رتبه</th><th>سقف خرید</th><th></th>
        </tr>
      </thead>
      <tbody>
        @for (c of filtered(); track c.id) {
          <tr>
            <td>{{ c.code }}</td>
            <td>{{ c.name }} {{ c.lastName }}</td>
            <td dir="ltr">{{ c.mob1 || c.phoneNum }}</td>
            <td>{{ cityName(c.city) }}</td>
            <td>{{ rateName(c.customerRate) }}</td>
            <td>{{ c.maxBuyPrice ? (c.maxBuyPrice | number) : '—' }}</td>
            <td class="actions">
              <button class="btn btn-ghost btn-sm" (click)="openEdit(c)">ویرایش</button>
              <button class="btn btn-danger btn-sm" (click)="remove(c)">حذف</button>
            </td>
          </tr>
        } @empty {
          <tr><td colspan="7"><div class="empty"><div class="icon">👥</div>هنوز مشتری‌ای ثبت نشده است</div></td></tr>
        }
      </tbody>
    </table>
  </div>

  @if (editing()) {
    <div class="modal-backdrop" (click)="close()">
      <div class="modal card" (click)="$event.stopPropagation()">
        <h3>{{ editing()!.id ? 'ویرایش مشتری' : 'مشتری جدید' }}</h3>

        @if (formError()) { <div class="alert alert-error">{{ formError() }}</div> }

        <form [formGroup]="form" (ngSubmit)="save()">
          <div class="grid-2">
            <div class="field"><label>کد *</label><input type="number" formControlName="code"></div>
            <div class="field"><label>نام *</label><input formControlName="name"></div>
            <div class="field"><label>نام خانوادگی *</label><input formControlName="lastName"></div>
            <div class="field"><label>نام پدر</label><input formControlName="fatherName"></div>
            <div class="field"><label>تلفن *</label><input formControlName="phoneNum"></div>
            <div class="field"><label>موبایل ۱</label><input formControlName="mob1"></div>
            <div class="field"><label>موبایل ۲</label><input formControlName="mob2"></div>
            <div class="field"><label>شهر *</label>
              <select formControlName="city">
                <option [ngValue]="''">انتخاب کنید…</option>
                @for (ct of cities(); track ct.id) { <option [value]="ct.id">{{ ct.name }}</option> }
              </select>
            </div>
            <div class="field"><label>رتبه مشتری *</label>
              <select formControlName="customerRate">
                <option [ngValue]="''">انتخاب کنید…</option>
                @for (r of rates(); track r.id) { <option [value]="r.id">{{ r.title }}</option> }
              </select>
            </div>
            <div class="field"><label>سقف خرید (تومان)</label><input type="number" formControlName="maxBuyPrice"></div>
          </div>
          <div class="field"><label>آدرس</label><textarea formControlName="address" rows="2"></textarea></div>
          <div class="field"><label>توضیحات</label><input formControlName="details"></div>

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
    .modal-backdrop {
      position: fixed; inset: 0; background: rgba(15,15,20,.5);
      display: grid; place-items: center; z-index: 100; padding: 16px;
    }
    .modal { width: 100%; max-width: 640px; max-height: 90vh; overflow-y: auto; }
    .modal h3 { margin-bottom: 16px; }
    .modal-actions { display: flex; justify-content: flex-end; gap: 8px; margin-top: 8px; }
  `],
})
export class CustomersComponent implements OnInit {
  private api = inject(CustomerApi);
  private lookups = inject(LookupsService);
  private fb = inject(FormBuilder);

  list = signal<Customer[]>([]);
  cities = signal<City[]>([]);
  rates = signal<CustomerRate[]>([]);
  error = signal<string | null>(null);
  formError = signal<string | null>(null);
  editing = signal<Customer | null>(null);
  saving = signal(false);
  search = signal('');

  form = this.fb.nonNullable.group({
    code: [0, Validators.required],
    name: ['', Validators.required],
    lastName: ['', Validators.required],
    fatherName: [''],
    phoneNum: ['', Validators.required],
    mob1: [''],
    mob2: [''],
    city: ['', Validators.required],
    customerRate: ['', Validators.required],
    maxBuyPrice: [null as number | null],
    address: [''],
    details: [''],
  });

  filtered = () => {
    const q = this.search().trim().toLowerCase();
    if (!q) return this.list();
    return this.list().filter(c =>
      `${c.name} ${c.lastName}`.toLowerCase().includes(q) || String(c.code).includes(q));
  };

  cityName(id: string) { return this.cities().find(c => c.id === id)?.name ?? '—'; }
  rateName(id: string) { return this.rates().find(r => r.id === id)?.title ?? '—'; }

  ngOnInit(): void {
    forkJoin({ customers: this.api.getAll(), cities: this.lookups.cities(), rates: this.lookups.rates() })
      .subscribe(({ customers, cities, rates }) => {
        this.list.set(customers ?? []);
        this.cities.set(cities ?? []);
        this.rates.set(rates ?? []);
      });
  }

  openNew(): void {
    this.formError.set(null);
    this.form.reset({ code: 0, name: '', lastName: '', fatherName: '', phoneNum: '', mob1: '', mob2: '', city: '', customerRate: '', maxBuyPrice: null, address: '', details: '' });
    this.editing.set({ id: '' } as Customer);
  }

  openEdit(c: Customer): void {
    this.formError.set(null);
    this.form.patchValue({
      code: c.code, name: c.name, lastName: c.lastName, fatherName: c.fatherName ?? '',
      phoneNum: c.phoneNum, mob1: c.mob1 ?? '', mob2: c.mob2 ?? '',
      city: c.city, customerRate: c.customerRate,
      maxBuyPrice: c.maxBuyPrice ?? null, address: c.address ?? '', details: c.details ?? '',
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
      code: Number(v.code), name: v.name, lastName: v.lastName, fatherName: v.fatherName,
      phoneNum: v.phoneNum, mob1: v.mob1, mob2: v.mob2,
      city: v.city, customerRate: v.customerRate,
      maxBuyPrice: v.maxBuyPrice ?? undefined, address: v.address, details: v.details,
    };
    const current = this.editing()!;
    const req = current.id
      ? this.api.update(current.id, payload)
      : this.api.create(payload);
    req.subscribe({
      next: () => { this.saving.set(false); this.close(); this.reload(); },
      error: (err: HttpErrorResponse) => {
        this.saving.set(false);
        this.formError.set(err.error?.toString() || 'ذخیره ناموفق بود');
      },
    });
  }

  remove(c: Customer): void {
    if (!confirm(`حذف مشتری «${c.name} ${c.lastName}»؟ این عمل قابل بازگشت نیست.`)) return;
    this.api.delete(c.id).subscribe({ next: () => this.reload() });
  }

  private reload(): void {
    forkJoin({ customers: this.api.getAll(), cities: this.lookups.cities(), rates: this.lookups.rates() })
      .subscribe(({ customers, cities, rates }) => {
        this.list.set(customers ?? []);
        this.cities.set(cities ?? []);
        this.rates.set(rates ?? []);
      });
  }
}
