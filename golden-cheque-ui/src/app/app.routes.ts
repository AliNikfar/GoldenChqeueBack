import { Routes } from '@angular/router';
import { authGuard } from './core/auth.guard';
import { LayoutComponent } from './layout/layout.component';

export const routes: Routes = [
  { path: 'login', loadComponent: () => import('./pages/login/login.component').then(m => m.LoginComponent) },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
      { path: '', pathMatch: 'full', loadComponent: () => import('./pages/dashboard/dashboard.component').then(m => m.DashboardComponent) },
      { path: 'customers', loadComponent: () => import('./pages/customers/customers.component').then(m => m.CustomersComponent) },
      { path: 'products', loadComponent: () => import('./pages/products/products.component').then(m => m.ProductsComponent) },
      { path: 'cheques', loadComponent: () => import('./pages/cheques/cheques.component').then(m => m.ChequesComponent) },
      { path: 'factors', loadComponent: () => import('./pages/factors/factors.component').then(m => m.FactorsComponent) },
      { path: 'banks', loadComponent: () => import('./pages/banks/banks.component').then(m => m.BanksComponent) },
      { path: 'lookups', loadComponent: () => import('./pages/lookups/lookups.component').then(m => m.LookupsComponent) },
    ],
  },
  { path: '**', redirectTo: '' },
];
