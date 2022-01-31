import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IPage } from 'src/app/models/page.model';
import { ActionGroup, IPlanItem, WoodAssortment } from 'src/app/models/plan-item.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlanItemService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

   }

  getPlanItems(subareaId: number | undefined, planId: number | undefined, pageIndex : number | undefined, pageSize: number | undefined): Observable<IPage<IPlanItem>> {

    let params = new HttpParams();

    if(pageIndex && pageSize) {
      params
        .append("PageIndex", pageIndex)
        .append("PageSize", pageSize);
    }

    if(subareaId) {
      params
        .append("SubareaId", subareaId);
    }

    if(planId) {
      params
        .append("PlanId", planId);
    }

    return this.http.get<IPage<IPlanItem>>(`${this.apiUrl}plan-items`, {params})
      .pipe(
        catchError(this.handleError<IPage<IPlanItem>>('getPlanItems', this.createBlankPage()))
      );
   }

  createBlankPage() : IPage<IPlanItem> {
    const blankPage : IPage<IPlanItem> = {
      pageIndex: 0,
      pageSize: 0,
      totalCount: 0,
      data: []
    }
    return blankPage;
  }

  createPlanItem(quantity : number, measureUnit : string, assortments: WoodAssortment,
    actionGroup : ActionGroup, difficultyLevel : number, factor : number,
    planId : number, subareaId : number) : Observable<Object> {

    return this.http.post(`${this.apiUrl}plan-items`, { quantity, measureUnit, assortments,
      actionGroup, difficultyLevel, factor, planId, subareaId });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
