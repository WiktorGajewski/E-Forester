import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IPlan } from 'src/app/models/plan.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlanService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

   }

  getPlans(forestUnitId: number | undefined): Observable<IPlan[]> {
      return this.http.get<IPlan[]>(`${this.apiUrl}plans`)
      .pipe(
        catchError(this.handleError<IPlan[]>('getPlans', []))
      );
   }

  createPlan(year : number, forestUnitId : number) : Observable<Object> {
    return this.http.post(`${this.apiUrl}plans`, { year, forestUnitId });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
