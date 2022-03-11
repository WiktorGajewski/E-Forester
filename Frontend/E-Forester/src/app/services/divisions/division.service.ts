import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IDivision } from 'src/app/models/division.model';
import { IPage } from 'src/app/models/page.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DivisionService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

  }

  getDivisions(forestUnitId: number | null, pageIndex: number | null, pageSize: number | null): Observable<IPage<IDivision>> {
    
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

    return this.http.get<IPage<IDivision>>(`${this.apiUrl}divisions`, {params})
      .pipe(
        catchError(this.handleError<IPage<IDivision>>('getDivisions', this.createBlankPage()))
      );
  }

  createBlankPage() : IPage<IDivision> {
    const blankPage : IPage<IDivision> = {
      pageIndex: 0,
      pageSize: 0,
      totalCount: 0,
      data: []
    }

    return blankPage;
  }

  createDivision(address : string, area : number, forestUnitId: number) : Observable<Object> {
    return this.http.post(`${this.apiUrl}divisions`, { address, area, forestUnitId });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
