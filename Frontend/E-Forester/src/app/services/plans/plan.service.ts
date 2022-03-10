import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IPage } from 'src/app/models/page.model';
import { IPlan } from 'src/app/models/plan.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlanService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

   }

  getPlans(forestUnitId: number | null, pageIndex : number | null, pageSize: number | null,
      yearFrom: number | null, yearTo: number | null): Observable<IPage<IPlan>> {

    let params = new HttpParams();

    if(pageIndex && pageSize) {
      params = params
        .append("PageIndex", pageIndex)
        .append("PageSize", pageSize);
    }

    if(yearFrom && yearTo) {
      params = params
        .append("YearFrom", yearFrom)
        .append("YearTo", yearTo);
    }

    if(forestUnitId) {
      params = params
        .append("ForestUnitId", forestUnitId);
    }

    return this.http.get<IPage<IPlan>>(`${this.apiUrl}plans`, {params})
      .pipe(
        catchError(this.handleError<IPage<IPlan>>('getPlans', this.createBlankPage()))
      );
  }

  getPlan(planId: number): Observable<IPlan> {

    return this.http.get<IPlan>(`${this.apiUrl}plans/${planId}`)
      .pipe(
        catchError(this.handleError<IPlan>('getPlan', undefined ))
      );
  }

  createBlankPage() : IPage<IPlan> {
    const blankPage : IPage<IPlan> = {
      pageIndex: 0,
      pageSize: 0,
      totalCount: 0,
      data: []
    }
    return blankPage;
  }

  createPlan(year : number, forestUnitId : number) : Observable<Object> {
    return this.http.post(`${this.apiUrl}plans`, { year, forestUnitId });
  }

  markPlanCompleted(planId : number) : Observable<Object> {
    return this.http.put(`${this.apiUrl}plans/${planId}/close`, {});
  }

  markPlanIncompleted(planId : number) : Observable<Object> {
    return this.http.put(`${this.apiUrl}plans/${planId}/open`, {});
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
