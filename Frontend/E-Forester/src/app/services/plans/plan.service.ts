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

  getPlans(forestUnitId: number | undefined, pageIndex : number | undefined, pageSize: number | undefined): Observable<IPage<IPlan>> {

    let params = new HttpParams();

    if(pageIndex && pageSize) {
      params
        .append("PageIndex", pageIndex)
        .append("PageSize", pageSize);
    }

    if(forestUnitId) {
      params
        .append("ForestUnitId", forestUnitId);
    }
    
    return this.http.get<IPage<IPlan>>(`${this.apiUrl}plans`, {params})
      .pipe(
        catchError(this.handleError<IPage<IPlan>>('getPlans', this.createBlankPage()))
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

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
