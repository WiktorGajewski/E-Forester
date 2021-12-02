import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IAuthentication } from './authentication.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  authorized = false;

  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { 
    const token = this.getToken();
    if(token != null) {
      this.authorized = true;
    }
  }

  login(login: string, password: string): Observable<IAuthentication> {
    
    return this.http.post<IAuthentication>(`${this.apiUrl}Account/LogIn`, { login, password })
      .pipe(map(result => {
        this.setSession(result)
        return result;
      }));
  }

  private setSession(result : IAuthentication): void {
    sessionStorage.setItem("accessToken", result.token);
  }

  getToken(): string | null {
    const token = sessionStorage.getItem("accessToken");
    return token;
  }
}
