import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IDivision } from 'src/app/models/division.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DivisionService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

  }

  getDivisions(): Observable<IDivision[]> {
    return this.http.get<IDivision[]>(`${this.apiUrl}divisions`)
    .pipe(
      catchError(this.handleError<IDivision[]>('getDivisions', []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
