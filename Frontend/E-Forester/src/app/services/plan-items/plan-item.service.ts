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

  getPlanItems(forestUnitId: number | null, divisionId: number | null, subareaId: number | null,
      planId: number | null, pageIndex : number | null, pageSize: number | null): Observable<IPage<IPlanItem>> {

    let params = new HttpParams();

    if(pageIndex && pageSize) {
      params = params
        .append("PageIndex", pageIndex)
        .append("PageSize", pageSize);
    }

    if(forestUnitId) {
      params = params
        .append("ForestUnitId", forestUnitId);
    }

    if(divisionId) {
      params = params
        .append("DivisionId", divisionId);
    }

    if(subareaId) {
      params = params
        .append("SubareaId", subareaId);
    }

    if(planId) {
      params = params
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

  createPlanItem(plannedHectares : number, plannedCubicMeters : number, assortments: WoodAssortment,
    actionGroup : ActionGroup, difficultyLevel : number, factor : number,
    planId : number, subareaId : number) : Observable<Object> {

    return this.http.post(`${this.apiUrl}plan-items`, { plannedHectares, plannedCubicMeters, assortments,
      actionGroup, difficultyLevel, factor, planId, subareaId });
  }

  markPlanItemsCompleted(planItemIds : number[]) : Observable<Object> {
    return this.http.put(`${this.apiUrl}plan-items/close`, { planItemIds });
  }

  markPlanItemsIncompleted(planItemIds : number[]) : Observable<Object> {
    return this.http.put(`${this.apiUrl}plan-items/open`, { planItemIds });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
