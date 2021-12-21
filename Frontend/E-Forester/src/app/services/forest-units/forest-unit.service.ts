import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ForestUnitService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

  }

  getForestUnits(): Observable<IForestUnit[]> {
    return this.http.get<IForestUnit[]>(`${this.apiUrl}forest-units`)
    .pipe(
      catchError(this.handleError<IForestUnit[]>('getForestUnits', []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
