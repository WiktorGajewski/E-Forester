import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IPage } from 'src/app/models/page.model';
import { ISubarea } from 'src/app/models/subarea.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SubareaService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

   }

  getSubareas(divisionId: number | null, pageIndex : number | null, pageSize: number | null): Observable<IPage<ISubarea>> {
    
    let params = new HttpParams();

    if(pageIndex && pageSize) {
      params = params
        .append("PageIndex", pageIndex)
        .append("PageSize", pageSize);
    }
    
    if(divisionId) {
      params = params
        .append("DivisionId", divisionId);
    }
    
    return this.http.get<IPage<ISubarea>>(`${this.apiUrl}subareas`, {params})
      .pipe(
        catchError(this.handleError<IPage<ISubarea>>('getSubareas', this.createBlankPage()))
      );
  }

  createBlankPage() : IPage<ISubarea> {
    const blankPage : IPage<ISubarea> = {
      pageIndex: 0,
      pageSize: 0,
      totalCount: 0,
      data: []
    }
    return blankPage;
  }

  createSubarea(address : string, area : number, divisionId : number) : Observable<Object> {
    return this.http.post(`${this.apiUrl}subareas`, { address, area, divisionId });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
