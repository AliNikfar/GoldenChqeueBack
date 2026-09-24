import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

import { AuthService } from '../auth.service';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  return next(req).pipe(
    catchError((err: unknown) => {
      if (err instanceof HttpErrorResponse) {
        // Expired/invalid JWT (the backend token lives only Jwt:DurationInMinutes
        // minutes): clear the stale session and send the user back to /login.
        if (err.status === 401 && !req.url.includes('/api/Auth/authenticate')) {
          inject(AuthService).logout();
          router.navigate(['/login']);
        }
        const serverMessage =
          typeof err.error === 'string' ? err.error
          : err.error?.message ?? err.error?.errors?.[0] ?? err.error?.title;
        const enriched = new HttpErrorResponse({
          error: serverMessage || `خطای سرور (${err.status})`,
          status: err.status,
          url: err.url ?? undefined,
        });
        return throwError(() => enriched);
      }
      return throwError(() => err);
    }),
  );
};
