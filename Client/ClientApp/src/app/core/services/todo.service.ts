import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { ApiConstants } from '../../shared/constants/api-constants';

@Injectable()
export class TodoService {

  constructor(private http: HttpClientService) { }

  getTodoListing(paginationRequest) {
    return this.http.post(`${ApiConstants.getTodoListing}`, paginationRequest )
      .pipe(
        catchError(error => throwError(error)),
        map((res: any) => {
          return res;
        }));
  }
  saveTodo(todo) {
  return this.http.post(`${ApiConstants.saveTodo}`, todo )
      .pipe(
        catchError(error => throwError(error)),
        map((res: any) => {
          return res;
        }));
  }
  deleteTodo(todoId) {
    return this.http.get(`${ApiConstants.deleteTodo}/${todoId}` )
        .pipe(
          catchError(error => throwError(error)),
          map((res: any) => {
            return res;
          }));
    }
    getTodoById(todoId) {
      return this.http.get(`${ApiConstants.getTodoById}/${todoId}`)
      .pipe(
        catchError(error => throwError(error)),
        map((res: any) => {
          return res;
        }));
    }
}
