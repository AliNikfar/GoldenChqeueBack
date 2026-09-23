import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

import { BankApi, LookupsService, ShobeApi } from '../../core/api.service';
import { Bank, Shobe } from '../../core/models';

@Component({
  selector: 'app-banks',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
  <div class="page-head">
    <div>
      <h2>بانک‌ها و شعب</h2>
      <div class="sub">تعریف بانک‌ها و شعب آن‌ها برای ثبت چک‌ها</div>
    </div>
  </div>

  @if (error()) { <div class="alert alert-error">{{ error() }}</div> }

  <div class="two-col">
    <div class="card">
      <h3 class="card-title">🏦 بانک‌ها</h3>
      <form [formGroup]="bankForm" (ngSubmit)="addBank()" class="add-row">
        <input formControlName="title" placeholder="نام بانک…">
        <button class="btn btn-primary btn-sm" [disabled]="bankForm.invalid">افزودن</button>
      </form>
      <div class="table-wrap" style="border:none">
        <table>
          <tbody>
            @for (b of banks(); track b.id) {
              <tr>
                <td>{{ b.title }}</td>
                <td class="actions">
                  <button class="btn btn-danger btn-sm" (click)="removeBank(b)">حذف</button>
                </td>
              </tr>
            } @empty {
              <tr><td><div class="empty" style="padding:20px">بانکی ثبت نشده</div></td></tr>
            }
          </tbody>
        </table>
      </div>
    </div>

    <div class="card">
      <h3 class="card-title">🏢 شعب</h3>
      <form [formGroup]="shobeForm" (ngSubmit)="addShobe()" class="add-row">
        <select formControlName="bankId">
          <option value="">بانک…</option>
          @for (b of banks(); track b.id) { <option [value]="b.id">{{ b.title }}</option> }
        </select>
        <input formControlName="name" placeholder="نام شعبه…">
        <input formControlName="code" placeholder="کد شعبه">
        <button class="btn btn-primary btn-sm" [disabled]="shobeForm.invalid">افزودن</button>
      </form>
      <div class="table-wrap" style="border:none">
        <table>
          <thead><tr><th>شعبه</th><th>کد</th><th>بانک</th><th></th></tr></thead>
          <tbody>
            @for (s of shobes(); track s.id) {
              <tr>
                <td>{{ s.name }}</td>
                <td>{{ s.code }}</td>
                <td>—</td>
                <td class="actions">
                  <button class="btn btn-danger btn-sm" (click)="removeShobe(s)">حذف</button>
                </td>
              </tr>
            } @empty {
              <tr><td colspan="4"><div class="empty" style="padding:20px">شعبه‌ای ثبت نشده</div></td></tr>
            }
          </tbody>
        </table>
      </div>
    </div>
  </div>
  `,
  styles: [`
    .two-col { display: grid; grid-template-columns: 1fr 1.4fr; gap: 16px; align-items: start; }
    @media (max-width: 900px) { .two-col { grid-template-columns: 1fr; } }
    .card-title { font-size: 15px; margin-bottom: 12px; }
    .add-row { display: flex; gap: 8px; margin-bottom: 12px; }
    .actions { display: flex; gap: 6px; justify-content: flex-end; }
  `],
})
export class BanksComponent implements OnInit {
  private bankApi = inject(BankApi);
  private shobeApi = inject(ShobeApi);
  private lookups = inject(LookupsService);
  private fb = inject(FormBuilder);

  banks = signal<Bank[]>([]);
  shobes = signal<Shobe[]>([]);
  error = signal<string | null>(null);

  bankForm = this.fb.nonNullable.group({ title: ['', Validators.required] });
  shobeForm = this.fb.nonNullable.group({
    bankId: ['', Validators.required],
    name: ['', Validators.required],
    code: ['', Validators.required],
  });

  ngOnInit(): void { this.reload(); }

  addBank(): void {
    if (this.bankForm.invalid) return;
    this.bankApi.create(this.bankForm.getRawValue()).subscribe({ next: () => {
      this.bankForm.reset({ title: '' });
      this.reload();
    }});
  }

  removeBank(b: Bank): void {
    if (!confirm(`حذف بانک «${b.title}»؟ شعب آن نیز حذف می‌شوند.`)) return;
    this.bankApi.delete(b.id).subscribe({ next: () => this.reload() });
  }

  addShobe(): void {
    if (this.shobeForm.invalid) return;
    this.shobeApi.create(this.shobeForm.getRawValue()).subscribe({ next: () => {
      this.shobeForm.reset({ bankId: '', name: '', code: '' });
      this.reload();
    }});
  }

  removeShobe(s: Shobe): void {
    if (!confirm(`حذف شعبه «${s.name}»؟`)) return;
    this.shobeApi.delete(s.id).subscribe({ next: () => this.reload() });
  }

  private reload(): void {
    forkJoin({ banks: this.bankApi.getAll(), shobes: this.lookups.shobes() })
      .subscribe({
        next: ({ banks, shobes }) => {
          this.banks.set(banks ?? []);
          this.shobes.set(shobes ?? []);
        },
        error: () => this.error.set('خطا در دریافت اطلاعات'),
      });
  }
}
