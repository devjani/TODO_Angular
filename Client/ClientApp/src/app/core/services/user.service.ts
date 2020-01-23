import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import { catchError, map } from 'rxjs/operators';
import { throwError, BehaviorSubject } from 'rxjs';
import { ApiConstants } from '../../shared/constants/api-constants';

@Injectable()
export class UserService {

  constructor(private http: HttpClientService) { }

  isUsernameExist(email: string) {
    return this.http.get(`${ApiConstants.isUsernameExist}?email=${email}`)
        .pipe(
            catchError(error => throwError(error)),
            map((response: any) => {
                return response.result;
            }));
}

  getUserListing(paginationRequest) {
    return this.http.post(`${ApiConstants.getUserListing}`, paginationRequest )
      .pipe(
        catchError(error => throwError(error)),
        map((res: any) => {
          return res;
        }));
  }

  getRoles(isActive) {
    return this.http.get(`${ApiConstants.getRoles}?isActive=${isActive}`)
      .pipe(
        catchError(error => throwError(error)),
        map((res: any) => {
          return res;
        }));
  }

  saveUsers(user) {
    return this.http.post(`${ApiConstants.saveUser}` , user )
    .pipe(
      catchError(error => throwError(error)),
      map((res: any) => {
        return res;
      }));
  }

  getUsersDetail(id) {
    return this.http.get(`${ApiConstants.getUserDetail}/${id}` )
    .pipe(
      catchError(error => throwError(error)),
      map((res: any) => {
        return res;
      }));
  }

  deleteUserDetail(id) {
    return this.http.get(`${ApiConstants.deleteUser}/${id}` )
    .pipe(
      catchError(error => throwError(error)),
      map((res: any) => {
        return res;
      }));
  }

}
