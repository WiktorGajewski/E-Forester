import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { IPage } from 'src/app/models/page.model';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ForestUnitService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

  }

  getForestUnits(pageIndex : number | null, pageSize: number | null): Observable<IPage<IForestUnit>> {

    let params = new HttpParams();

    if(pageIndex && pageSize) {
      params = params
        .append("PageIndex", pageIndex)
        .append("PageSize", pageSize);
    }

    return this.http.get<IPage<IForestUnit>>(`${this.apiUrl}forest-units`, {params})
      .pipe(
        catchError(this.handleError<IPage<IForestUnit>>('getForestUnits', this.createBlankPage()))
      );
  }

  createBlankPage() : IPage<IForestUnit> {
    const blankPage : IPage<IForestUnit> = {
      pageIndex: 0,
      pageSize: 0,
      totalCount: 0,
      data: []
    }
    return blankPage;
  }

  createForestUnit(name : string, address : string, area : number) : Observable<Object> {
    return this.http.post(`${this.apiUrl}forest-units`, { name, address, area });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
