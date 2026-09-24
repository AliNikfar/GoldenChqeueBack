import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { AuthService } from '../../core/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  template: `
  <div class="auth-wrap">
    <div class="auth-card">
      <div class="auth-logo">🥇</div>
      <h1 class="auth-title">گلدن‌شک</h1>
      <p class="auth-sub">برای ورود به پنل مدیریت، اطلاعات حساب خود را وارد کنید</p>

      @if (error()) {
        <div class="alert alert-error">{{ error() }}</div>
      }

      <form [formGroup]="form" (ngSubmit)="submit()">
        <div class="field">
          <label>ایمیل</label>
          <input type="email" formControlName="email" placeholder="you@example.com" autocomplete="username">
        </div>
        <div class="field">
          <label>رمز عبور</label>
          <input type="password" formControlName="password" placeholder="••••••••" autocomplete="current-password">
        </div>

        <button class="btn btn-primary" style="width:100%; justify-content:center" [disabled]="loading() || form.invalid">
          @if (loading()) { در حال ورود... } @else { ورود }
        </button>
      </form>

      <p class="hint">حساب پیش‌فرض seed شده: superadmin&#64;gmail.com</p>
    </div>
  </div>
  `,
  styles: [`
    .hint { text-align: center; color: var(--text-muted); font-size: 12px; margin-top: 18px; }
    form { display: flex; flex-direction: column; gap: 4px; }
  `],
})
export class LoginComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthService);
  private router = inject(Router);

  loading = signal(false);
  error = signal<string | null>(null);

  form = this.fb.nonNullable.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
  });

  submit(): void {
    if (this.form.invalid || this.loading()) return;
    this.loading.set(true);
    this.error.set(null);
    const { email, password } = this.form.getRawValue();
    this.auth.login(email, password).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigateByUrl('/');
      },
      error: (err: HttpErrorResponse) => {
        this.loading.set(false);
        this.error.set(err.error?.toString() || 'ورود ناموفق بود. اطلاعات را بررسی کنید.');
      },
    });
  }
}
