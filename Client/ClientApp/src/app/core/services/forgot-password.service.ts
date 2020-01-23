import { Injectable } from '@angular/core';
import { HttpClientService } from './http-client.service';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { ApiConstants } from '../../shared/constants/api-constants';

@Injectable()
export class ForgotPasswordService {
    constructor(private httpClientService: HttpClientService) { }

    sendForgotPwdEmail(email: string) {
        return this.httpClientService.get(`${ApiConstants.sendForgotPasswordEmail}?email=${email}`)
            .pipe(
                catchError(error => throwError(error)),
                map((response: any) => {
                    return response.result;
                }));
    }

    resetPassword(pwdData) {
        return this.httpClientService.post(`${ApiConstants.resetPassword}`, pwdData)
            .pipe(
                catchError(error => throwError(error)),
                map((response: any) => {
                    return response.result;
                }));
    }

}
