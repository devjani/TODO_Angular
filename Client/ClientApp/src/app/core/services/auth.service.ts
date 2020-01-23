import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { HttpClientService } from './http-client.service';
import { ApiConstants } from '../../shared/constants/api-constants';
import { User, LoggedInUserDetails } from '../../models/user';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  jwtToken = 'auth-token';
  refreshTokens = 'refresh-token';
  userInfo = 'user-data-auth';
  rememberUser = 'remember-user';

  constructor(private httpService: HttpClientService) { }

  getAuthToken() {
    return localStorage.getItem(this.jwtToken) || null;
  }

  setAuhToken(token: string) {
    localStorage.setItem(this.jwtToken, token);
  }

  setRefreshToken(refreshToken: string) {
    localStorage.setItem(this.refreshTokens, refreshToken);
  }

  getRefreshToken() {
    return localStorage.getItem(this.refreshTokens) || null;
  }

  destroyToken() {
    window.localStorage.removeItem(this.jwtToken);
  }

  getRememberUser() {
    return localStorage.getItem(this.rememberUser) || null;
  }

  setRememberUser(rememberValue: boolean) {
    localStorage.setItem(this.rememberUser, rememberValue.toString());
  }

  refreshToken(): Observable<any> {
    return this.httpService.post(ApiConstants.refreshToken, { RefreshToken: this.getRefreshToken() });
  }

  setUserInfoAuth(userInfo: any) {
    localStorage.setItem(
      this.userInfo,
      userInfo ? btoa(JSON.stringify(userInfo)) : ''
    );
  }

  getUserInfoAuth(): User {
    return this.userInfo
      ? ((localStorage.getItem(this.userInfo) !== null && localStorage.getItem(this.userInfo) !== undefined &&
        localStorage.getItem(this.userInfo) !== '') ? (JSON.parse(atob(localStorage.getItem(this.userInfo)))) : null)
      : null;
  }

  getUserData(key: string) {
    const data = this.decodeToken(localStorage.getItem(this.jwtToken));
    return data[key];
  }

  createUserInfo(user: LoggedInUserDetails) {
    return {
      Id: user.id,
      Email: user.email,
      Name: user.name,
    };
  }
  validateToken(token: string) {
    return this.httpService.get(`${ApiConstants.validateToken}?token=${token}`)
      .pipe(
        catchError(error => throwError(error)),
        map((response: any) => {
          return response;
        }));
  }
  logout() {
    localStorage.removeItem(this.jwtToken);
    localStorage.removeItem(this.userInfo);
    localStorage.removeItem(this.refreshTokens);
  }

  private decodeToken(token) {
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch (error) {
      return {};
    }
  }
  isCustomerLogin() {
    if (this.getUserInfoAuth()) {
    return true;
    }
    return false;
  }
}
