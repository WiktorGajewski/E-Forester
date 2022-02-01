import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IPage } from 'src/app/models/page.model';
import { IPlanExecution } from 'src/app/models/plan-execution.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlanExecutionService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

  }

  getPlanExecutions(planItemId: number | undefined, planId: number | undefined, pageIndex : number | undefined, pageSize: number | undefined): Observable<IPage<IPlanExecution>> {

    let params = new HttpParams();

    if(pageIndex && pageSize) {
      params
        .append("PageIndex", pageIndex)
        .append("PageSize", pageSize);
    }

    if(planItemId) {
      params
        .append("PlanItemId", planItemId);
    }

    if(planId) {
      params
        .append("PlanId", planId);
    }

    return this.http.get<IPage<IPlanExecution>>(`${this.apiUrl}plan-executions`, {params})
      .pipe(
        catchError(this.handleError<IPage<IPlanExecution>>('getPlanExecutions', this.createBlankPage()))
      );
  }

  createBlankPage() : IPage<IPlanExecution> {
    const blankPage : IPage<IPlanExecution> = {
      pageIndex: 0,
      pageSize: 0,
      totalCount: 0,
      data: []
    }
    return blankPage;
  }

  createPlanExecution(executedHectares : number, harvestedCubicMeters : number,planItemId : number, planId: number) : Observable<Object> {
    return this.http.post(`${this.apiUrl}plan-executions`, { executedHectares, harvestedCubicMeters, planItemId, planId });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
