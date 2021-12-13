import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, throwError } from "rxjs";
import { AuthService } from "../auth/auth.service";

@Injectable({
    providedIn: 'root'
})
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) {
        
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if([401, 403].includes(err.status) && this.authService.authenticationValue?.accessToken) {
                this.authService.logout();
            }

            const error = err?.error?.message || err?.statusText;
            return throwError(() => error);
        }))
    }
}