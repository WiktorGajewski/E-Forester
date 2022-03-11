import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { AuthService } from "../services/auth/auth.service";

@Injectable({
    providedIn: 'root'
})
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) {

    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const accessToken = this.authService.authenticationValue?.accessToken;
        const isApiUrl = request.url.startsWith(environment.apiUrl);

        if(accessToken && isApiUrl) {
            request = request.clone({
                setHeaders: { Authorization: `Bearer ${accessToken}` }
            });
        }

        return next.handle(request);
    }

}