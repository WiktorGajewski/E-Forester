import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { ISubarea } from 'src/app/models/subarea.model';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SubareaService {
  private readonly apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {

   }

   getSubareas(): Observable<ISubarea[]> {
     return this.http.get<ISubarea[]>(`${this.apiUrl}subareas`)
      .pipe(
        catchError(this.handleError<ISubarea[]>('getSubareas', []))
      );
   }

   private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    };
  }
}
