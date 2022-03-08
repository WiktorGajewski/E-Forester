import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { IPage } from 'src/app/models/page.model';
import { IUser } from 'src/app/models/user.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getUsers(pageIndex : number | null, pageSize: number | null): Observable<IPage<IUser>> {

    let params = new HttpParams();

    if(pageIndex && pageSize) {
      params = params
        .append("PageIndex", pageIndex)
        .append("PageSize", pageSize);
    }

    return this.http.get<IPage<IUser>>(`${this.apiUrl}users`, {params})
      .pipe(
        catchError(this.handleError<IPage<IUser>>('getUsers', this.createBlankPage()))
      );
  }

  createBlankPage() : IPage<IUser> {
    const blankPage : IPage<IUser> = {
      pageIndex: 0,
      pageSize: 0,
      totalCount: 0,
      data: []
    }
    return blankPage;
  }

  registerUser(name : string, login : string, password : string, role : number) : Observable<Object> {
    return this.http.post(`${this.apiUrl}account/register`, { name, login, password, role });
  }

  assignForestUnit(userId: number, forestUnitId: number) : Observable<Object> {
    return this.http.post(`${this.apiUrl}users/forestUnits`, { userId, forestUnitId });
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}