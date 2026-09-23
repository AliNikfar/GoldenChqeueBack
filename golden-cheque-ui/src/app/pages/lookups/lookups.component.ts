import { Component, OnInit, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { forkJoin } from 'rxjs';

import { LookupsService, LookupAdminApi } from '../../core/api.service';
import { Category, City, CustomerRate, State, Unit } from '../../core/models';

@Component({
  selector: 'app-lookups',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
  <div class="page-head">
    <div>
      <h2>اطلاعات پایه</h2>
      <div class="sub">استان‌ها، شهرها، واحدها، دسته‌بندی‌ها و رتبه مشتری</div>
    </div>
  </div>

  <div class="grid-3">
    <!-- states -->
    <div class="card">
      <h3 class="card-title">🗺️ استان‌ها</h3>
      <form [formGroup]="stateForm" (ngSubmit)="addState()" class="add-row">
        <input formControlName="name" placeholder="نام استان…">
        <button class="btn btn-primary btn-sm" [disabled]="stateForm.invalid">+</button>
      </form>
      <ul class="simple-list">
        @for (s of states(); track s.id) {
          <li><span>{{ s.name }}</span><button class="del" (click)="delState(s)">×</button></li>
        } @empty { <li class="muted">موردی نیست</li> }
      </ul>
    </div>

    <!-- cities -->
    <div class="card">
      <h3 class="card-title">🏙️ شهرها</h3>
      <form [formGroup]="cityForm" (ngSubmit)="addCity()" class="add-row">
        <select formControlName="ostan">
          <option value="">استان…</option>
          @for (s of states(); track s.id) { <option [value]="s.id">{{ s.name }}</option> }
        </select>
        <input formControlName="name" placeholder="نام شهر…">
        <input formControlName="cityCode" placeholder="کد">
        <button class="btn btn-primary btn-sm" [disabled]="cityForm.invalid">+</button>
      </form>
      <ul class="simple-list">
        @for (c of cities(); track c.id) {
          <li><span>{{ c.name }} <small>({{ cityName(c.ostan) }})</small></span><button class="del" (click)="delCity(c)">×</button></li>
        } @empty { <li class="muted">موردی نیست</li> }
      </ul>
    </div>

    <!-- rates -->
    <div class="card">
      <h3 class="card-title">⭐ رتبه مشتری</h3>
      <form [formGroup]="rateForm" (ngSubmit)="addRate()" class="add-row">
        <input formControlName="title" placeholder="مثلاً طلایی…">
        <button class="btn btn-primary btn-sm" [disabled]="rateForm.invalid">+</button>
      </form>
      <ul class="simple-list">
        @for (r of rates(); track r.id) {
          <li><span>{{ r.title }}</span></li>
        } @empty { <li class="muted">موردی نیست</li> }
      </ul>
    </div>

    <!-- units -->
    <div class="card">
      <h3 class="card-title">📐 واحدها</h3>
      <form [formGroup]="unitForm" (ngSubmit)="addUnit()" class="add-row">
        <input formControlName="name" placeholder="نام واحد…">
        <input type="number" formControlName="quantityPerUnit" placeholder="تعداد">
        <button class="btn btn-primary btn-sm" [disabled]="unitForm.invalid">+</button>
      </form>
      <ul class="simple-list">
        @for (u of units(); track u.id) {
          <li><span>{{ u.name }} <small>({{ u.quantityPerUnit }} عددی)</small></span><button class="del" (click)="delUnit(u)">×</button></li>
        } @empty { <li class="muted">موردی نیست</li> }
      </ul>
    </div>

    <!-- categories -->
    <div class="card" style="grid-column: 1 / -1">
      <h3 class="card-title">🗂️ دسته‌بندی‌ها</h3>
      <form [formGroup]="catForm" (ngSubmit)="addCategory()" class="add-row">
        <select formControlName="parentId">
          <option value="">دسته والد (بدون والد)</option>
          @for (c of categories(); track c.id) { <option [value]="c.id">{{ c.title }}</option> }
        </select>
        <input formControlName="title" placeholder="نام دسته…">
        <button class="btn btn-primary btn-sm" [disabled]="catForm.invalid">+</button>
      </form>
      <ul class="simple-list">
        @for (c of categories(); track c.id) {
          <li>
            <span>{{ c.parentId ? '↳ ' : '' }}{{ c.title }}</span>
            <button class="del" (click)="delCategory(c)">×</button>
          </li>
        } @empty { <li class="muted">موردی نیست</li> }
      </ul>
    </div>
  </div>
  `,
  styles: [`
    .grid-3 { display: grid; grid-template-columns: repeat(auto-fit, minmax(280px, 1fr)); gap: 16px; }
    .card-title { font-size: 15px; margin-bottom: 12px; }
    .add-row { display: flex; gap: 8px; margin-bottom: 12px; }
    .add-row input, .add-row select { min-width: 0; }
    .simple-list { list-style: none; margin: 0; padding: 0; max-height: 260px; overflow-y: auto; }
    .simple-list li {
      display: flex; justify-content: space-between; align-items: center;
      padding: 8px 10px; border-bottom: 1px solid var(--ink-100); font-size: 13.5px;
    }
    .simple-list li:last-child { border-bottom: none; }
    .simple-list small { color: var(--text-muted); }
    .muted { color: var(--text-muted); justify-content: center; }
    .del {
      border: none; background: #fee2e2; color: var(--red-500);
      width: 24px; height: 24px; border-radius: 8px; cursor: pointer; font-size: 14px; line-height: 1;
    }
    .del:hover { background: #fecaca; }
  `],
})
export class LookupsComponent implements OnInit {
  private lookups = inject(LookupsService);
  private admin = inject(LookupAdminApi);
  private fb = inject(FormBuilder);

  states = signal<State[]>([]);
  cities = signal<City[]>([]);
  rates = signal<CustomerRate[]>([]);
  units = signal<Unit[]>([]);
  categories = signal<Category[]>([]);

  stateForm = this.fb.nonNullable.group({ name: ['', Validators.required] });
  cityForm = this.fb.nonNullable.group({
    ostan: ['', Validators.required], name: ['', Validators.required], cityCode: ['', Validators.required],
  });
  rateForm = this.fb.nonNullable.group({ title: ['', Validators.required] });
  unitForm = this.fb.nonNullable.group({
    name: ['', Validators.required], quantityPerUnit: [1, Validators.required],
  });
  catForm = this.fb.nonNullable.group({ parentId: [''], title: ['', Validators.required] });

  cityName(id: string) { return this.states().find(s => s.id === id)?.name ?? '—'; }

  ngOnInit(): void { this.reload(); }

  addState(): void {
    if (this.stateForm.invalid) return;
    this.admin.createState(this.stateForm.getRawValue()).subscribe({ next: () => {
      this.stateForm.reset({ name: '' }); this.reload();
    }});
  }
  delState(s: State): void {
    if (!confirm(`حذف استان «${s.name}»؟`)) return;
    this.admin.deleteState(s.id).subscribe({ next: () => this.reload() });
  }

  addCity(): void {
    if (this.cityForm.invalid) return;
    this.admin.createCity(this.cityForm.getRawValue()).subscribe({ next: () => {
      this.cityForm.reset({ ostan: '', name: '', cityCode: '' }); this.reload();
    }});
  }
  delCity(c: City): void {
    if (!confirm(`حذف شهر «${c.name}»؟`)) return;
    this.admin.deleteCity(c.id).subscribe({ next: () => this.reload() });
  }

  addRate(): void {
    if (this.rateForm.invalid) return;
    this.admin.createRate(this.rateForm.getRawValue()).subscribe({ next: () => {
      this.rateForm.reset({ title: '' }); this.reload();
    }});
  }

  addUnit(): void {
    if (this.unitForm.invalid) return;
    this.admin.createUnit(this.unitForm.getRawValue()).subscribe({ next: () => {
      this.unitForm.reset({ name: '', quantityPerUnit: 1 }); this.reload();
    }});
  }
  delUnit(u: Unit): void {
    if (!confirm(`حذف واحد «${u.name}»؟`)) return;
    this.admin.deleteUnit(u.id).subscribe({ next: () => this.reload() });
  }

  addCategory(): void {
    if (this.catForm.invalid) return;
    const v = this.catForm.getRawValue();
    this.admin.createCategory({ title: v.title, parentId: v.parentId || null }).subscribe({ next: () => {
      this.catForm.reset({ parentId: '', title: '' }); this.reload();
    }});
  }
  delCategory(c: Category): void {
    if (!confirm(`حذف دسته «${c.title}»؟`)) return;
    this.admin.deleteCategory(c.id).subscribe({ next: () => this.reload() });
  }

  private reload(): void {
    forkJoin({
      states: this.lookups.states(), cities: this.lookups.cities(), rates: this.lookups.rates(),
      units: this.lookups.units(), categories: this.lookups.categories(),
    }).subscribe(({ states, cities, rates, units, categories }) => {
      this.states.set(states ?? []);
      this.cities.set(cities ?? []);
      this.rates.set(rates ?? []);
      this.units.set(units ?? []);
      this.categories.set(categories ?? []);
    });
  }
}
