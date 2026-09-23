import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { DecimalPipe } from '@angular/common';
import { forkJoin } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

import { LookupsService, ProductApi } from '../../core/api.service';
import { Category, Product, Unit } from '../../core/models';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [ReactiveFormsModule, DecimalPipe],
  template: `
  <div class="page-head">
    <div>
      <h2>کالاها</h2>
      <div class="sub">مدیریت کالاها، قیمت‌ها و موجودی انبار</div>
    </div>
    <button class="btn btn-primary" (click)="openNew()">+ کالای جدید</button>
  </div>

  @if (error()) { <div class="alert alert-error">{{ error() }}</div> }

  <div class="table-wrap">
    <table>
      <thead>
        <tr><th>عنوان</th><th>قیمت فروش</th><th>قیمت خرید</th><th>واحد</th><th>دسته</th><th>موجودی</th><th></th></tr>
      </thead>
      <tbody>
        @for (p of list(); track p.id) {
          <tr>
            <td>{{ p.title }}</td>
            <td>{{ p.price | number }}</td>
            <td>{{ p.buyPrice | number }}</td>
            <td>{{ p.unit?.name ?? '—' }}</td>
            <td>{{ p.category?.title ?? '—' }}</td>
            <td>
              <span class="badge" [class.badge-green]="p.wareHouseStock > 0" [class.badge-red]="p.wareHouseStock <= 0">
                {{ p.wareHouseStock }}
              </span>
            </td>
            <td class="actions">
              <button class="btn btn-ghost btn-sm" (click)="openEdit(p)">ویرایش</button>
              <button class="btn btn-danger btn-sm" (click)="remove(p)">حذف</button>
            </td>
          </tr>
        } @empty {
          <tr><td colspan="7"><div class="empty"><div class="icon">📦</div>کالایی ثبت نشده است</div></td></tr>
        }
      </tbody>
    </table>
  </div>

  @if (editing()) {
    <div class="modal-backdrop" (click)="close()">
      <div class="modal card" (click)="$event.stopPropagation()">
        <h3>{{ editing()!.id ? 'ویرایش کالا' : 'کالای جدید' }}</h3>
        @if (formError()) { <div class="alert alert-error">{{ formError() }}</div> }

        <form [formGroup]="form" (ngSubmit)="save()">
          <div class="grid-2">
            <div class="field" style="grid-column: 1 / -1"><label>عنوان *</label><input formControlName="title"></div>
            <div class="field"><label>قیمت فروش *</label><input type="number" formControlName="price"></div>
            <div class="field"><label>قیمت خرید *</label><input type="number" formControlName="buyPrice"></div>
            <div class="field"><label>واحد *</label>
              <select formControlName="unitId">
                <option value="">انتخاب کنید…</option>
                @for (u of units(); track u.id) { <option [value]="u.id">{{ u.name }} ({{ u.quantityPerUnit }})</option> }
              </select>
            </div>
            <div class="field"><label>دسته‌بندی *</label>
              <select formControlName="categoryId">
                <option value="">انتخاب کنید…</option>
                @for (c of categories(); track c.id) { <option [value]="c.id">{{ c.title }}</option> }
              </select>
            </div>
            <div class="field"><label>موجودی انبار *</label><input type="number" formControlName="wareHouseStock"></div>
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
    .modal { width: 100%; max-width: 560px; max-height: 90vh; overflow-y: auto; }
    .modal h3 { margin-bottom: 16px; }
    .modal-actions { display: flex; justify-content: flex-end; gap: 8px; margin-top: 8px; }
  `],
})
export class ProductsComponent implements OnInit {
  private api = inject(ProductApi);
  private lookups = inject(LookupsService);
  private fb = inject(FormBuilder);

  list = signal<Product[]>([]);
  units = signal<Unit[]>([]);
  categories = signal<Category[]>([]);
  error = signal<string | null>(null);
  formError = signal<string | null>(null);
  editing = signal<Product | null>(null);
  saving = signal(false);

  form = this.fb.nonNullable.group({
    title: ['', Validators.required],
    price: [0, Validators.required],
    buyPrice: [0, Validators.required],
    unitId: ['', Validators.required],
    categoryId: ['', Validators.required],
    wareHouseStock: [0, Validators.required],
  });

  ngOnInit(): void {
    this.reload();
  }

  openNew(): void {
    this.formError.set(null);
    this.form.reset({ title: '', price: 0, buyPrice: 0, unitId: '', categoryId: '', wareHouseStock: 0 });
    this.editing.set({ id: '' } as Product);
  }

  openEdit(p: Product): void {
    this.formError.set(null);
    this.form.patchValue({
      title: p.title, price: p.price, buyPrice: p.buyPrice,
      unitId: p.unit?.id ?? '', categoryId: p.category?.id ?? '',
      wareHouseStock: p.wareHouseStock,
    });
    this.editing.set(p);
  }

  close(): void { this.editing.set(null); }

  save(): void {
    if (this.form.invalid) return;
    this.saving.set(true);
    this.formError.set(null);
    const v = this.form.getRawValue();
    const current = this.editing()!;
    const req = current.id
      ? this.api.update(current.id, v)
      : this.api.create({ ...v, imageId: null });
    req.subscribe({
      next: () => { this.saving.set(false); this.close(); this.reload(); },
      error: (err: HttpErrorResponse) => {
        this.saving.set(false);
        this.formError.set(err.error?.toString() || 'ذخیره ناموفق بود');
      },
    });
  }

  remove(p: Product): void {
    if (!confirm(`حذف کالای «${p.title}»؟`)) return;
    this.api.delete(p.id).subscribe({ next: () => this.reload() });
  }

  private reload(): void {
    forkJoin({ products: this.api.getAll(), units: this.lookups.units(), categories: this.lookups.categories() })
      .subscribe(({ products, units, categories }) => {
        this.list.set(products ?? []);
        this.units.set(units ?? []);
        this.categories.set(categories ?? []);
      });
  }
}
