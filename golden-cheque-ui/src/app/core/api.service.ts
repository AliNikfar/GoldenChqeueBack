import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import {
  Bank, Category, Cheque, City, Customer, CustomerRate,
  Factor, Ghest, Product, Shobe, State, Unit,
} from './models';

/** Raw API responses sometimes omit camelCase normalization; keep permissive types. */
function api<T>(http: HttpClient, url: string): Observable<T> {
  return http.get<T>(url);
}

@Injectable({ providedIn: 'root' })
export class LookupsService {
  private http = inject(HttpClient);
  states = () => api<State[]>(this.http, '/api/State');
  cities = () => api<City[]>(this.http, '/api/City');
  rates = () => api<CustomerRate[]>(this.http, '/api/CustomerRate');
  units = () => api<Unit[]>(this.http, '/api/Unit');
  categories = () => api<Category[]>(this.http, '/api/Category');
  banks = () => api<Bank[]>(this.http, '/api/Bank');
  shobes = () => api<Shobe[]>(this.http, '/api/Shobe');
  customers = () => api<Customer[]>(this.http, '/api/Customer');
}

@Injectable({ providedIn: 'root' })
export class CustomerApi {
  private http = inject(HttpClient);
  getAll = () => api<Customer[]>(this.http, '/api/Customer');
  getById = (id: string) => api<Customer>(this.http, `/api/Customer/${id}`);
  create = (c: Partial<Customer>) => this.http.post('/api/Customer', c);
  update = (id: string, c: Partial<Customer>) => this.http.put(`/api/Customer/${id}`, c);
  delete = (id: string) => this.http.delete(`/api/Customer/${id}`);
}

@Injectable({ providedIn: 'root' })
export class ProductApi {
  private http = inject(HttpClient);
  getAll = () => api<Product[]>(this.http, '/api/Product');
  create = (p: Partial<Product> & { unitId: string; categoryId: string; imageId?: string | null }) =>
    this.http.post('/api/Product', p);
  update = (id: string, p: Partial<Product>) => this.http.put(`/api/Product/${id}`, p);
  delete = (id: string) => this.http.delete(`/api/Product/${id}`);
}

@Injectable({ providedIn: 'root' })
export class BankApi {
  private http = inject(HttpClient);
  getAll = () => api<Bank[]>(this.http, '/api/Bank');
  create = (b: { title: string }) => this.http.post('/api/Bank', b);
  update = (id: string, b: { title: string }) => this.http.put(`/api/Bank/${id}`, b);
  delete = (id: string) => this.http.delete(`/api/Bank/${id}`);
}

@Injectable({ providedIn: 'root' })
export class ShobeApi {
  private http = inject(HttpClient);
  getAll = () => api<Shobe[]>(this.http, '/api/Shobe');
  create = (s: Partial<Shobe> & { bankId: string }) => this.http.post('/api/Shobe', s);
  update = (id: string, s: Partial<Shobe>) => this.http.put(`/api/Shobe/${id}`, s);
  delete = (id: string) => this.http.delete(`/api/Shobe/${id}`);
}

@Injectable({ providedIn: 'root' })
export class ChequeApi {
  private http = inject(HttpClient);
  getAll = () => api<Cheque[]>(this.http, '/api/Cheque');
  create = (c: Partial<Cheque>) => this.http.post('/api/Cheque', c);
  update = (id: string, c: Partial<Cheque>) => this.http.put(`/api/Cheque/${id}`, c);
  delete = (id: string) => this.http.delete(`/api/Cheque/${id}`);
}

@Injectable({ providedIn: 'root' })
export class FactorApi {
  private http = inject(HttpClient);
  getAll = () => api<Factor[]>(this.http, '/api/Factor');
  getById = (id: string) => api<Factor>(this.http, `/api/Factor/${id}`);
  create = (f: Partial<Factor>) => this.http.post('/api/Factor', f);
  update = (id: string, f: Partial<Factor>) => this.http.put(`/api/Factor/${id}`, f);
  delete = (id: string) => this.http.delete(`/api/Factor/${id}`);
  ghestsByFactor = (id: string) => api<Ghest[]>(this.http, `/api/Ghest/factor/${id}`);
  addGhest = (g: Partial<Ghest>) => this.http.post('/api/Ghest', g);
  updateGhest = (id: string, g: Partial<Ghest>) => this.http.put(`/api/Ghest/${id}`, g);
  deleteGhest = (id: string) => this.http.delete(`/api/Ghest/${id}`);
}

@Injectable({ providedIn: 'root' })
export class LookupAdminApi {
  private http = inject(HttpClient);
  // states
  createState = (s: { name: string }) => this.http.post('/api/State', s);
  deleteState = (id: string) => this.http.delete(`/api/State/${id}`);
  // cities
  createCity = (c: { name: string; cityCode: string; ostan: string }) => this.http.post('/api/City', c);
  deleteCity = (id: string) => this.http.delete(`/api/City/${id}`);
  // rates
  createRate = (r: { title: string }) => this.http.post('/api/CustomerRate', r);
  // units
  createUnit = (u: { name: string; quantityPerUnit: number }) => this.http.post('/api/Unit', u);
  updateUnit = (id: string, u: { name: string; quantityPerUnit: number }) => this.http.put(`/api/Unit/${id}`, u);
  deleteUnit = (id: string) => this.http.delete(`/api/Unit/${id}`);
  // categories
  createCategory = (c: { title: string; parentId?: string | null }) => this.http.post('/api/Category', c);
  updateCategory = (id: string, c: { title: string; parentId?: string | null }) => this.http.put(`/api/Category/${id}`, c);
  deleteCategory = (id: string) => this.http.delete(`/api/Category/${id}`);
}
