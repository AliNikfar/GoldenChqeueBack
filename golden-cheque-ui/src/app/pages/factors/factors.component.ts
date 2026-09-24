import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DatePipe, DecimalPipe } from '@angular/common';
import { forkJoin } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

import { CustomerApi, FactorApi } from '../../core/api.service';
import { Customer, Factor, Ghest } from '../../core/models';

@Component({
  selector: 'app-factors',
  standalone: true,
  imports: [ReactiveFormsModule, DatePipe, DecimalPipe],
  template: `
  <div class="page-head">
    <div>
      <h2>فاکتورها و اقساط</h2>
      <div class="sub">ثبت فاکتور فروش و مدیریت اقساط هر فاکتور</div>
    </div>
    <button class="btn btn-primary" (click)="openNew()">+ فاکتور جدید</button>
  </div>

  @if (error()) { <div class="alert alert-error">{{ error() }}</div> }

  <div class="table-wrap">
    <table>
      <thead>
        <tr>
          <th>شماره</th><th>مشتری</th><th>مبلغ کل</th><th>درصد سود</th>
          <th>تاریخ خرید</th><th>مانده</th><th></th>
        </tr>
      </thead>
      <tbody>
        @for (f of list(); track f.id) {
          <tr (click)="openGhests(f)" style="cursor:pointer">
            <td><span class="badge badge-gold">{{ shortId(f.id) }}</span></td>
            <td>{{ customerName(f.personCode) }}</td>
            <td>{{ f.factorSumPrice | number }}</td>
            <td>٪{{ f.factorSodDarsad }}</td>
            <td>{{ f.factorKharidDate | date:'yyyy/MM/dd' }}</td>
            <td>{{ (f.factorSumPrice - f.factorBeforePrice) | number }}</td>
            <td class="actions" (click)="$event.stopPropagation()">
              <button class="btn btn-ghost btn-sm" (click)="openGhests(f)">اقساط</button>
              <button class="btn btn-ghost btn-sm" (click)="openEdit(f)">ویرایش</button>
              <button class="btn btn-danger btn-sm" (click)="remove(f)">حذف</button>
            </td>
          </tr>
        } @empty {
          <tr><td colspan="7"><div class="empty"><div class="icon">💳</div>فاکتوری ثبت نشده است</div></td></tr>
        }
      </tbody>
    </table>
  </div>

  <!-- create / edit factor -->
  @if (editing()) {
    <div class="modal-backdrop" (click)="close()">
      <div class="modal card" (click)="$event.stopPropagation()">
        <h3>{{ editing()!.id ? 'ویرایش فاکتور' : 'فاکتور جدید' }}</h3>
        @if (formError()) { <div class="alert alert-error">{{ formError() }}</div> }

        <form [formGroup]="form" (ngSubmit)="save()">
          <div class="grid-2">
            <div class="field"><label>مشتری *</label>
              <select formControlName="personCode">
                <option value="">انتخاب کنید…</option>
                @for (c of customers(); track c.id) { <option [value]="c.id">{{ c.name }} {{ c.lastName }}</option> }
              </select>
            </div>
            <div class="field"><label>نوع *</label>
              <select formControlName="kind">
                <option [ngValue]="0">نقدی</option>
                <option [ngValue]="1">اقساطی</option>
                <option [ngValue]="2">چکی</option>
              </select>
            </div>
            <div class="field"><label>مبلغ کل (تومان) *</label><input type="number" formControlName="factorSumPrice"></div>
            <div class="field"><label>درصد سود *</label><input type="number" formControlName="factorSodDarsad"></div>
            <div class="field"><label>جمع اقلام (تومان)</label><input type="number" formControlName="factorSumObjectsPrice"></div>
            <div class="field"><label>مانده قبلی (تومان)</label><input type="number" formControlName="factorBeforePrice"></div>
            <div class="field"><label>تاریخ خرید *</label><input type="date" formControlName="factorKharidDate"></div>
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

  <!-- installments of a factor -->
  @if (ghestsOf()) {
    <div class="modal-backdrop" (click)="closeGhests()">
      <div class="modal card" (click)="$event.stopPropagation()">
        <h3>اقساط فاکتور {{ shortId(ghestsOf()!.id) }} — {{ customerName(ghestsOf()!.personCode) }}</h3>
        @if (ghestError()) { <div class="alert alert-error">{{ ghestError() }}</div> }

        <div class="table-wrap" style="margin-bottom:16px">
          <table>
            <thead><tr><th>مبلغ</th><th>تاریخ قسط</th><th>سررسید</th><th>وضعیت</th><th></th></tr></thead>
            <tbody>
              @for (g of ghestList(); track g.id) {
                <tr>
                  <td>{{ g.price | number }}</td>
                  <td>{{ g.date | date:'yyyy/MM/dd' }}</td>
                  <td>{{ g.passDate | date:'yyyy/MM/dd' }}</td>
                  <td>
                    <button class="badge {{ g.status ? 'badge-green' : 'badge-amber' }}"
                            style="cursor:pointer;border:none"
                            (click)="toggleGhest(g)">
                      {{ g.status ? 'پرداخت شده' : 'در انتظار' }}
                    </button>
                  </td>
                  <td><button class="btn btn-danger btn-sm" (click)="removeGhest(g)">حذف</button></td>
                </tr>
              } @empty {
                <tr><td colspan="5"><div class="empty"><div class="icon">🗓️</div>قسطی ثبت نشده — اولین قسط را اضافه کنید</div></td></tr>
              }
            </tbody>
          </table>
        </div>

        <form [formGroup]="ghestForm" (ngSubmit)="addGhest()" class="ghest-add">
          <div class="field"><label>مبلغ قسط *</label><input type="number" formControlName="price"></div>
          <div class="field"><label>تاریخ قسط *</label><input type="date" formControlName="date"></div>
          <div class="field"><label>سررسید *</label><input type="date" formControlName="passDate"></div>
          <button class="btn btn-primary" [disabled]="ghestForm.invalid || addingGhest()">
            {{ addingGhest() ? '…' : '+ افزودن قسط' }}
          </button>
        </form>

        <div class="modal-actions">
          <button class="btn btn-ghost" (click)="closeGhests()">بستن</button>
        </div>
      </div>
    </div>
  }
  `,
  styles: [`
    .actions { display: flex; gap: 6px; }
    .modal-backdrop { position: fixed; inset: 0; background: rgba(15,15,20,.5); display: grid; place-items: center; z-index: 100; padding: 16px; }
    .modal { width: 100%; max-width: 720px; max-height: 90vh; overflow-y: auto; }
    .modal h3 { margin-bottom: 16px; }
    .modal-actions { display: flex; justify-content: flex-end; gap: 8px; margin-top: 12px; }
    .ghest-add { display: grid; grid-template-columns: 1fr 1fr 1fr auto; gap: 10px; align-items: end; }
    @media (max-width: 700px) { .ghest-add { grid-template-columns: 1fr 1fr; } }
  `],
})
export class FactorsComponent implements OnInit {
  private api = inject(FactorApi);
  private customerApi = inject(CustomerApi);
  private fb = inject(FormBuilder);

  list = signal<Factor[]>([]);
  customers = signal<Customer[]>([]);
  error = signal<string | null>(null);
  formError = signal<string | null>(null);
  editing = signal<Factor | null>(null);
  saving = signal(false);

  ghestsOf = signal<Factor | null>(null);
  ghestList = signal<Ghest[]>([]);
  ghestError = signal<string | null>(null);
  addingGhest = signal(false);

  form = this.fb.nonNullable.group({
    personCode: ['', Validators.required],
    kind: [0, Validators.required],
    factorSumPrice: [0, Validators.required],
    factorSodDarsad: [0, Validators.required],
    factorSumObjectsPrice: [0],
    factorBeforePrice: [0],
    factorKharidDate: ['', Validators.required],
  });

  ghestForm = this.fb.nonNullable.group({
    price: [0, Validators.required],
    date: [new Date().toISOString().slice(0, 10), Validators.required],
    passDate: [new Date().toISOString().slice(0, 10), Validators.required],
  });

  shortId(id: string) { return id?.slice(0, 8).toUpperCase(); }
  customerName(id: string) { const c = this.customers().find(x => x.id === id); return c ? `${c.name} ${c.lastName}` : '—'; }

  ngOnInit(): void { this.reload(); }

  openNew(): void {
    this.formError.set(null);
    this.form.reset({
      personCode: '', kind: 0, factorSumPrice: 0, factorSodDarsad: 0,
      factorSumObjectsPrice: 0, factorBeforePrice: 0,
      factorKharidDate: new Date().toISOString().slice(0, 10),
    });
    this.editing.set({ id: '' } as Factor);
  }

  openEdit(f: Factor): void {
    this.formError.set(null);
    this.form.patchValue({
      personCode: f.personCode, kind: f.kind, factorSumPrice: f.factorSumPrice,
      factorSodDarsad: f.factorSodDarsad, factorSumObjectsPrice: f.factorSumObjectsPrice,
      factorBeforePrice: f.factorBeforePrice,
      factorKharidDate: f.factorKharidDate?.slice(0, 10),
    });
    this.editing.set(f);
  }

  close(): void { this.editing.set(null); }

  save(): void {
    if (this.form.invalid) return;
    this.saving.set(true);
    this.formError.set(null);
    const v = this.form.getRawValue();
    const payload = {
      personCode: v.personCode, kind: Number(v.kind),
      factorSumPrice: Number(v.factorSumPrice), factorSodDarsad: Number(v.factorSodDarsad),
      factorSumObjectsPrice: Number(v.factorSumObjectsPrice), factorBeforePrice: Number(v.factorBeforePrice),
      factorKharidDate: new Date(v.factorKharidDate).toISOString(), visable: true,
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

  remove(f: Factor): void {
    if (!confirm('حذف این فاکتور؟ اقساط مرتبط نیز حذف می‌شود.')) return;
    this.api.delete(f.id).subscribe({ next: () => this.reload() });
  }

  // ---------- installments ----------
  openGhests(f: Factor): void {
    this.ghestsOf.set(f);
    this.ghestError.set(null);
    this.api.ghestsByFactor(f.id).subscribe({
      next: gs => this.ghestList.set(gs ?? []),
      error: () => this.ghestList.set([]),
    });
  }

  closeGhests(): void { this.ghestsOf.set(null); }

  addGhest(): void {
    const f = this.ghestsOf();
    if (!f || this.ghestForm.invalid) return;
    this.addingGhest.set(true);
    this.ghestError.set(null);
    const v = this.ghestForm.getRawValue();
    this.api.addGhest({
      price: Number(v.price), status: false,
      date: new Date(v.date).toISOString(), passDate: new Date(v.passDate).toISOString(),
      factor: f.id,
    }).subscribe({
      next: () => {
        this.addingGhest.set(false);
        this.ghestForm.patchValue({ price: 0 });
        this.openGhests(f);
      },
      error: (err: HttpErrorResponse) => {
        this.addingGhest.set(false);
        this.ghestError.set(err.error?.toString() || 'افزودن قسط ناموفق بود');
      },
    });
  }

  toggleGhest(g: Ghest): void {
    const f = this.ghestsOf();
    if (!f) return;
    this.api.updateGhest(g.id, {
      price: g.price, status: !g.status,
      date: g.date, passDate: g.passDate, factor: f.id,
    }).subscribe({ next: () => this.openGhests(f) });
  }

  removeGhest(g: Ghest): void {
    const f = this.ghestsOf();
    if (!f || !confirm('حذف این قسط؟')) return;
    this.api.deleteGhest(g.id).subscribe({ next: () => this.openGhests(f) });
  }

  private reload(): void {
    forkJoin({ factors: this.api.getAll(), customers: this.customerApi.getAll() })
      .subscribe(({ factors, customers }) => {
        this.list.set(factors ?? []);
        this.customers.set(customers ?? []);
      });
  }
}
