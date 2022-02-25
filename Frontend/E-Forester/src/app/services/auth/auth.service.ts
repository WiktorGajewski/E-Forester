import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IAuthentication } from '../../models/authentication.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public authenticationSubject: BehaviorSubject<IAuthentication|null>;
  public authentication: Observable<IAuthentication|null>;

  private refreshTokenTimeout: ReturnType<typeof setTimeout> | undefined;

  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private router: Router) { 
    this.authenticationSubject = new BehaviorSubject<IAuthentication|null>(null);
    this.authentication = this.authenticationSubject.asObservable();
  }

  public get authenticationValue(): IAuthentication|null {
    return this.authenticationSubject.value;
  }

  login(login: string, password: string): Observable<IAuthentication> {
    
    return this.http.post<IAuthentication>(`${this.apiUrl}account/login`, { login, password }, { withCredentials: true })
      .pipe(map(result => {
        this.setSession(result);
        this.startRefreshTokenTimer();
        return result;
      }));
  }

  logout(): void {
    this.http.post(`${this.apiUrl}account/revoke-token`, {}, { withCredentials: true }).subscribe();
    this.clearSession();
    this.stopRefreshTokenTimer();
  }

  refreshToken(): Observable<IAuthentication> {
    return this.http.post<IAuthentication>(`${this.apiUrl}account/refresh-token`, {}, { withCredentials: true })
      .pipe(map(result => {
        this.setSession(result);
        this.startRefreshTokenTimer();
        return result;
      }));
  }

  private setSession(result : IAuthentication): void {
    this.authenticationSubject.next(result);
  }

  private clearSession(): void {
    this.authenticationSubject.next(null);
  }

  private startRefreshTokenTimer(): void {
    const accessToken = this.authenticationSubject.value?.accessToken;

    if(accessToken != null) {
      const expiry = (JSON.parse(atob(accessToken.split('.')[1]))).exp;

      const expires = new Date(expiry * 1000);
      const timeout = expires.getTime() - Date.now() - (60 * 1000);

      this.refreshTokenTimeout = setTimeout(() => this.refreshToken().subscribe(), timeout);
    }
  }

  private stopRefreshTokenTimer() {

    if(this.refreshTokenTimeout) {
      clearTimeout(this.refreshTokenTimeout);
    }
  }
}
