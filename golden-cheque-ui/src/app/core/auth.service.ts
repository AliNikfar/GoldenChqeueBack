import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

export interface AuthResponse {
  succeeded: boolean;
  message?: string;
  data?: {
    id: string;
    userName: string;
    firstName: string;
    email: string;
    roles: string[];
    jwToken: string;
  };
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly tokenKey = 'gc_token';
  private readonly userKey = 'gc_user';

  currentUser = signal<{ userName: string; firstName: string; roles: string[] } | null>(this.readUser());

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<AuthResponse> {
    return this.http.post<AuthResponse>('/api/Auth/authenticate', { email, password })
      .pipe(tap(res => {
        if (res.succeeded && res.data) {
          localStorage.setItem(this.tokenKey, res.data.jwToken);
          localStorage.setItem(this.userKey, JSON.stringify({
            userName: res.data.userName,
            firstName: res.data.firstName,
            roles: res.data.roles ?? [],
          }));
          this.currentUser.set({ userName: res.data.userName, firstName: res.data.firstName, roles: res.data.roles ?? [] });
        }
      }));
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    localStorage.removeItem(this.userKey);
    this.currentUser.set(null);
  }

  get token(): string | null { return localStorage.getItem(this.tokenKey); }

  isLoggedIn(): boolean { return !!this.token; }

  private readUser(): { userName: string; firstName: string; roles: string[] } | null {
    try {
      const raw = localStorage.getItem(this.userKey);
      return raw ? JSON.parse(raw) : null;
    } catch { return null; }
  }
}
