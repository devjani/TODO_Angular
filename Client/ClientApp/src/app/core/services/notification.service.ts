import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable()
export class NotificationService {

    constructor(private toastr: ToastrService) { }

    success(message, title = '') {
        this.toastr.success(
            message,
            title,
            { timeOut: 7000 }
        );
    }

    error(message = 'Sorry, something went wrong. Please try again.', title = 'Error', errorShow?: any) {
        if (errorShow) {
            setTimeout(() => {
                this.toastr.error(
                    message,
                    title,
                    { timeOut: 7000 }
                );
            });
        } else {
            this.toastr.error(
                message,
                title,
                { timeOut: 7000 }
            );
        }
    }

    info(message, title = '') {
        this.toastr.info(
            message,
            title,
            { timeOut: 7000 }
        );
    }

    warn(message, title = '') {
        this.toastr.warning(
            message,
            title,
            { timeOut: 7000 }
        );
    }

}
