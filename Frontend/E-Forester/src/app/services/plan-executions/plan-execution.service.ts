import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IPlanExecution } from 'src/app/models/plan-execution.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlanExecutionService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

   }

  getPlanExecutions(): Observable<IPlanExecution[]> {
     return this.http.get<IPlanExecution[]>(`${this.apiUrl}plan-executions`)
      .pipe(
        catchError(this.handleError<IPlanExecution[]>('getPlanExecutions', []))
      );
   }

  createPlanExecution(quantity : number, planItemId : number, planId: number) : Observable<Object> {
    return this.http.post(`${this.apiUrl}plan-executions`, { quantity, planItemId, planId });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
