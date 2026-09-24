import { Component, computed, inject, signal } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../core/auth.service';

interface NavItem { path: string; label: string; icon: string; }

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `
  <div class="shell">
    <aside class="sidebar" [class.open]="sidebarOpen()">
      <div class="brand">
        <span class="brand-logo">🥇</span>
        <div>
          <div class="brand-name">گلدن‌شک</div>
          <div class="brand-sub">مدیریت چک و اقساط</div>
        </div>
      </div>

      <nav>
        @for (item of nav; track item.path) {
          <a [routerLink]="item.path" routerLinkActive="active" (click)="sidebarOpen.set(false)">
            <span class="ic">{{ item.icon }}</span>{{ item.label }}
          </a>
        }
      </nav>

      <div class="sidebar-footer">
        <div class="who">
          <div class="avatar">{{ initial() }}</div>
          <div>
            <div class="who-name">{{ auth.currentUser()?.firstName || auth.currentUser()?.userName }}</div>
            <div class="who-role">{{ role() }}</div>
          </div>
        </div>
        <button class="btn btn-ghost btn-sm w-full" (click)="logout()">خروج از حساب</button>
      </div>
    </aside>

    @if (sidebarOpen()) {
      <div class="backdrop" (click)="sidebarOpen.set(false)"></div>
    }

    <main class="content">
      <button class="menu-btn btn btn-ghost btn-sm" (click)="sidebarOpen.set(true)">☰</button>
      <router-outlet />
    </main>
  </div>
  `,
  styles: [`
    .shell { display: flex; min-height: 100vh; }

    .sidebar {
      width: 250px;
      background: var(--sidebar-bg);
      color: #d6d3d1;
      display: flex;
      flex-direction: column;
      padding: 18px 14px;
      position: sticky;
      top: 0;
      height: 100vh;
      z-index: 40;
    }

    .brand { display: flex; align-items: center; gap: 10px; padding: 4px 8px 18px; }
    .brand-logo {
      width: 42px; height: 42px; border-radius: 12px;
      background: linear-gradient(135deg, var(--gold-400), var(--gold-600));
      display: grid; place-items: center; font-size: 21px;
      box-shadow: 0 8px 18px -6px rgba(171,117,38,.55);
    }
    .brand-name { color: #fff; font-weight: 800; font-size: 16px; }
    .brand-sub { font-size: 11px; color: #a8a29e; }

    nav { display: flex; flex-direction: column; gap: 4px; flex: 1; }
    nav a {
      display: flex; align-items: center; gap: 10px;
      padding: 10px 12px;
      border-radius: 10px;
      color: #d6d3d1;
      font-size: 13.5px;
      font-weight: 500;
      transition: background .15s, color .15s;
    }
    nav a:hover { background: rgba(255,255,255,.06); color: #fff; }
    nav a.active { background: linear-gradient(90deg, rgba(199,146,47,.25), rgba(199,146,47,.08)); color: var(--gold-300); }
    .ic { width: 20px; text-align: center; }

    .sidebar-footer { border-top: 1px solid rgba(255,255,255,.08); padding-top: 12px; display: flex; flex-direction: column; gap: 10px; }
    .who { display: flex; align-items: center; gap: 10px; }
    .avatar {
      width: 36px; height: 36px; border-radius: 50%;
      background: var(--gold-600); color: #fff;
      display: grid; place-items: center; font-weight: 700;
    }
    .who-name { color: #fff; font-size: 13px; font-weight: 600; }
    .who-role { font-size: 11px; color: #a8a29e; }
    .w-full { width: 100%; justify-content: center; }
    .sidebar-footer .btn-ghost { border-color: rgba(255,255,255,.15); color: #d6d3d1; }
    .sidebar-footer .btn-ghost:hover { background: rgba(255,255,255,.08); }

    .backdrop { display: none; }
    .menu-btn { display: none; }

    .content { flex: 1; padding: 26px 30px; max-width: 1200px; margin: 0 auto; width: 100%; }

    @media (max-width: 900px) {
      .sidebar { position: fixed; right: -260px; transition: right .25s ease; box-shadow: -10px 0 40px rgba(0,0,0,.4); }
      .sidebar.open { right: 0; }
      .backdrop { display: block; position: fixed; inset: 0; background: rgba(0,0,0,.45); z-index: 35; }
      .menu-btn { display: inline-flex; margin-bottom: 14px; }
      .content { padding: 18px 16px; }
    }
  `],
})
export class LayoutComponent {
  auth = inject(AuthService);
  sidebarOpen = signal(false);

  nav: NavItem[] = [
    { path: '/', label: 'داشبورد', icon: '🏠' },
    { path: '/customers', label: 'مشتریان', icon: '👥' },
    { path: '/products', label: 'کالاها', icon: '📦' },
    { path: '/cheques', label: 'چک‌ها', icon: '🧾' },
    { path: '/factors', label: 'فاکتورها و اقساط', icon: '💳' },
    { path: '/banks', label: 'بانک‌ها و شعب', icon: '🏦' },
    { path: '/lookups', label: 'اطلاعات پایه', icon: '⚙️' },
  ];

  initial = computed(() => {
    const n = this.auth.currentUser()?.firstName || this.auth.currentUser()?.userName || '؟';
    return n.trim().charAt(0);
  });

  role = computed(() => {
    const roles = this.auth.currentUser()?.roles ?? [];
    const map: Record<string, string> = {
      SuperAdmin: 'مدیر ارشد', Admin: 'مدیر', Moderator: 'اپراتور', Basic: 'کاربر',
    };
    return roles.length ? map[roles[0]] ?? roles[0] : 'کاربر';
  });

  logout(): void { this.auth.logout(); }
}
