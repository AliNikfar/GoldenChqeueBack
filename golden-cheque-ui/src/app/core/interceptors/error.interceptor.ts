import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  return next(req).pipe(
    catchError((err: unknown) => {
      if (err instanceof HttpErrorResponse) {
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
