import { Component, OnInit, computed, inject, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DecimalPipe, DatePipe } from '@angular/common';
import { forkJoin } from 'rxjs';

import { ChequeApi, CustomerApi, FactorApi, ProductApi } from '../../core/api.service';
import { Cheque, Customer, Factor, Product } from '../../core/models';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink, DecimalPipe, DatePipe],
  template: `
  <div class="page-head">
    <div>
      <h2>داشبورد</h2>
      <div class="sub">نمای کلی سیستم در یک نگاه</div>
    </div>
    <a routerLink="/factors" class="btn btn-primary btn-sm">فاکتور جدید</a>
  </div>

  <div class="kpis">
    <div class="kpi">
      <div class="kpi-ic" style="background:#fef3c7">👥</div>
      <div>
        <div class="kpi-num">{{ customers().length.toLocaleString('fa-IR') }}</div>
        <div class="kpi-lbl">مشتری</div>
      </div>
    </div>
    <div class="kpi">
      <div class="kpi-ic" style="background:#dbeafe">📦</div>
      <div>
        <div class="kpi-num">{{ products().length.toLocaleString('fa-IR') }}</div>
        <div class="kpi-lbl">کالا</div>
      </div>
    </div>
    <div class="kpi">
      <div class="kpi-ic" style="background:#dcfce7">🧾</div>
      <div>
        <div class="kpi-num">{{ cheques().length.toLocaleString('fa-IR') }}</div>
        <div class="kpi-lbl">چک ثبت‌شده</div>
      </div>
    </div>
    <div class="kpi">
      <div class="kpi-ic" style="background:#f3e8ff">💳</div>
      <div>
        <div class="kpi-num">{{ factors().length.toLocaleString('fa-IR') }}</div>
        <div class="kpi-lbl">فاکتور</div>
      </div>
    </div>
  </div>

  <div class="two-col">
    <div class="card">
      <h3 class="card-title">⏰ چک‌های سررسید نزدیک (۳۰ روز آینده)</h3>
      @if (dueCheques().length === 0) {
        <div class="empty"><div class="icon">✅</div>چک سررسیدنزدیکی وجود ندارد</div>
      } @else {
        <div class="table-wrap" style="border:none">
          <table>
            <thead>
              <tr><th>شماره چک</th><th>مبلغ</th><th>سررسید</th></tr>
            </thead>
            <tbody>
              @for (c of dueCheques(); track c.id) {
                <tr>
                  <td>{{ c.shomareChek }}</td>
                  <td>{{ c.chequePrice | number }}</td>
                  <td>{{ c.passDate | date:'yyyy/MM/dd' }}</td>
                </tr>
              }
            </tbody>
          </table>
        </div>
      }
    </div>

    <div class="card">
      <h3 class="card-title">💳 اقساط سررسید نزدیک</h3>
      @if (dueGhests().length === 0) {
        <div class="empty"><div class="icon">✅</div>قسط سررسیدنزدیکی وجود ندارد</div>
      } @else {
        <div class="table-wrap" style="border:none">
          <table>
            <thead>
              <tr><th>مبلغ قسط</th><th>سررسید</th><th>وضعیت</th></tr>
            </thead>
            <tbody>
              @for (g of dueGhests(); track g.id) {
                <tr>
                  <td>{{ g.price | number }}</td>
                  <td>{{ g.passDate | date:'yyyy/MM/dd' }}</td>
                  <td>
                    <span class="badge" [class.badge-green]="g.status" [class.badge-amber]="!g.status">
                      {{ g.status ? 'پرداخت شده' : 'در انتظار' }}
                    </span>
                  </td>
                </tr>
              }
            </tbody>
          </table>
        </div>
      }
    </div>
  </div>
  `,
  styles: [`
    .kpis { display: grid; grid-template-columns: repeat(auto-fit, minmax(160px, 1fr)); gap: 14px; margin-bottom: 20px; }
    .kpi {
      background: var(--surface); border: 1px solid var(--border); border-radius: var(--radius);
      padding: 16px; display: flex; align-items: center; gap: 12px; box-shadow: var(--shadow-sm);
    }
    .kpi-ic { width: 46px; height: 46px; border-radius: 12px; display: grid; place-items: center; font-size: 22px; }
    .kpi-num { font-size: 22px; font-weight: 800; }
    .kpi-lbl { font-size: 12.5px; color: var(--text-muted); }
    .two-col { display: grid; grid-template-columns: 1fr 1fr; gap: 16px; }
    @media (max-width: 900px) { .two-col { grid-template-columns: 1fr; } }
    .card-title { font-size: 15px; margin-bottom: 12px; }
  `],
})
export class DashboardComponent implements OnInit {
  private chequeApi = inject(ChequeApi);
  private customerApi = inject(CustomerApi);
  private productApi = inject(ProductApi);
  private factorApi = inject(FactorApi);

  customers = signal<Customer[]>([]);
  products = signal<Product[]>([]);
  cheques = signal<Cheque[]>([]);
  factors = signal<Factor[]>([]);
  ghests = signal<{ id: string; price: number; status: boolean; passDate: string }[]>([]);

  dueCheques = computed(() => {
    const now = Date.now();
    const in30 = now + 30 * 864e5;
    return this.cheques()
      .filter(c => {
        const d = new Date(c.passDate).getTime();
        return d >= now && d <= in30;
      })
      .sort((a, b) => +new Date(a.passDate) - +new Date(b.passDate))
      .slice(0, 8);
  });

  dueGhests = computed(() => {
    const now = Date.now();
    const in30 = now + 30 * 864e5;
    return this.ghests()
      .filter(g => {
        const d = new Date(g.passDate).getTime();
        return d >= now && d <= in30;
      })
      .sort((a, b) => +new Date(a.passDate) - +new Date(b.passDate))
      .slice(0, 8);
  });

  ngOnInit(): void {
    forkJoin({
      customers: this.customerApi.getAll(),
      products: this.productApi.getAll(),
      cheques: this.chequeApi.getAll(),
      factors: this.factorApi.getAll(),
    }).subscribe({
      next: ({ customers, products, cheques, factors }) => {
        this.customers.set(customers ?? []);
        this.products.set(products ?? []);
        this.cheques.set(cheques ?? []);
        this.factors.set(factors ?? []);
      },
      error: () => { /* intercepted errors surface globally */ },
    });

    // aggregate installments of all factors (small dataset: acceptable for now)
    this.factorApi.getAll().subscribe(factors => {
      (factors ?? []).forEach(f => {
        this.factorApi.ghestsByFactor(f.id).subscribe(gs => {
          this.ghests.update(list => [...list, ...(gs ?? []).map(g => ({
            id: g.id, price: g.price, status: g.status, passDate: g.passDate,
          }))]);
        });
      });
    });
  }
}
