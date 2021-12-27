import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { ActionGroup, IPlanItem, WoodAssortment } from 'src/app/models/plan-item.model';
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
