import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { AuthService } from './auth.service';
import { NotificationService } from './notification.service';
import { ApiConstants } from '../../shared/constants/api-constants';
import { LoginViewModel } from '../../shared/models/login-view-model';


@Injectable()
export class LoginService {

  constructor(
    private httpClientService: HttpClientService,
  ) { }

  public getToken(): any {
    return this.httpClientService.get('api/login');
  }

  login(loginDetails: LoginViewModel) {
    return this.httpClientService.post(`${ApiConstants.login}`, loginDetails)
      .pipe(
        catchError(error => throwError(error)),
        map((res: any) => {
          return res;
        }));
  }
}
