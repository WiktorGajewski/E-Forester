import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IPlanItem } from 'src/app/models/plan-item.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlanItemService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

   }

   getPlanItems(): Observable<IPlanItem[]> {
     return this.http.get<IPlanItem[]>(`${this.apiUrl}plan-items`)
      .pipe(
        catchError(this.handleError<IPlanItem[]>('getPlanItems', []))
      );
   }

   private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
