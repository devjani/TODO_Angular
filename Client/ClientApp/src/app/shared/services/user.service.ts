
import { ChangePasswordModel } from '../models/change-password-model';
import { ApiConstants } from '../constants/api-constants';
import { catchError, map } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../../core/services/event-service';
import { EventConstants } from '../constants/event-constants';
import { HttpClientService } from '../../core/services';
import { ForgotPasswordModel } from '../models/forgot-password-model';

@Injectable()
export class UserService {
    title: string;
    constructor(
        private httpClientService: HttpClientService,
        private activatedRoute: ActivatedRoute,
        private eventService: EventService
    ) { }

    changePassword(changePassword: ChangePasswordModel) {
        return this.httpClientService.post(`${ApiConstants.changePassword}`, changePassword)
            .pipe(
                catchError(error => throwError(error)),
                map((response: any) => {
                    return response;
                }));
    }

    // send email to registered email address to restore the password
    sendForgotPasswordEmail(email: string) {
        return this.httpClientService.get(`${ApiConstants.sendForgotPasswordEmail}?email=${email}`)
            .pipe(
                catchError(error => throwError(error)),
                map((response: any) => {
                    return response.result;
                }));
    }

    setPassword(setPasswordModel: ForgotPasswordModel) {
        return this.httpClientService.post(ApiConstants.resetPassword, setPasswordModel)
            .pipe(
                catchError(error => throwError(error)),
                map((response: any) => {
                    return response;
                }));
    }



    validateToken(token: string) {
        return this.httpClientService.get(`${ApiConstants.validateToken}?token=${token}`)
            .pipe(
                catchError(error => throwError(error)),
                map((response: any) => {
                    return response;
                }));
    }

    validateResetPasswordToken(token: string) {
        return this.httpClientService.get(`${ApiConstants.validateForgotPasswordToken}?token=${token}`)
            .pipe(
                catchError(error => throwError(error)),
                map((response: any) => {
                    return response;
                }));
    }


    isUsernameExist(username: string) {
        return this.httpClientService.post(`${ApiConstants.isUsernameExist}?username=${username}`, null)
            .pipe(
                catchError(error => throwError(error)),
                map((response: any) => {
                    return response;
                }));
    }

    getAllLanguages() {
        return this.httpClientService.get(ApiConstants.getAllLanguages)
            .pipe(
                catchError(error => throwError(error)),
                map((response: any) => {
                    return response.result;
                }));
    }
}
