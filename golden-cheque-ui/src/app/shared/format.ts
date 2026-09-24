import { Pipe, PipeTransform } from '@angular/core';

/** 1234567 -> «۱٬۲۳۴٬۵۶۷ تومان» style currency text (keeps latin digits by default). */
@Pipe({ name: 'money', standalone: true })
export class MoneyPipe implements PipeTransform {
  transform(value: number | null | undefined, currency = ' تومان'): string {
    if (value === null || value === undefined || Number.isNaN(value)) return '—';
    return new Intl.NumberFormat('fa-IR').format(value) + currency;
  }
}

/** ISO/UTC date -> Persian (Jalali) readable date. */
@Pipe({ name: 'jdate', standalone: true })
export class JDatePipe implements PipeTransform {
  transform(value: string | Date | null | undefined, withTime = false): string {
    if (!value) return '—';
    const d = typeof value === 'string' ? new Date(value) : value;
    if (Number.isNaN(d.getTime())) return '—';
    return new Intl.DateTimeFormat('fa-IR', {
      year: 'numeric', month: '2-digit', day: '2-digit',
      ...(withTime ? { hour: '2-digit', minute: '2-digit' } : {}),
    }).format(d);
  }
}
