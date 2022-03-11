import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../services/auth/auth.service";

@Injectable({
    providedIn: 'root'
})
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {
        
    }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        const authentication = this.authService.authenticationValue;

        if(authentication?.accessToken) {

            if(route.data["roles"] && route.data["roles"].indexOf(authentication.userRole) === -1) {
                this.router.navigate(['/']);
                return false;
            }

            return true;
        } else {
            this.router.navigate(['/login']);
            return false;
        }
    }
    
}