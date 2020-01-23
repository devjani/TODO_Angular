import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators, FormGroup, FormControl, NgForm } from '@angular/forms';
import { UserService } from '../../../core/services/user.service';
import { Location } from '@angular/common';
import { ForgotPasswordService } from '../../../core/services/forgot-password.service';
import { NotificationService } from 'src/app/core/services/notification.service';
import { AppConstants } from 'src/app/shared/constants/app-constants';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {
  isFormSubmitted: boolean;
  emailRequiredErrorText: string;
  username: string;
  emailPattern = AppConstants.emailPattern;
  emailFormatValidationText: string;
  @ViewChild('forgotPasswordForm', null) forgotPasswordForm: NgForm;

  constructor(
    private userService: UserService,
    private forgotPasswordService: ForgotPasswordService,
    private notify: NotificationService,
    private location: Location
  ) { }

  ngOnInit() {
    this.emailFormatValidationText = 'EmailPatternError';
    this.emailRequiredErrorText = 'REQUIRED_EMAIL';
    this.username = '';
    this.isFormSubmitted = false;
  }

  onSubmit() {
    this.isFormSubmitted = true;
    if (this.forgotPasswordForm.valid) {
      this.userService.isUsernameExist(this.username).subscribe((res: any) => {
        if (res) {
          this.sendForgotPasswordEmail();
        } else {
          this.notify.error('Email is not registered');
        }
      }, (error) => {
        // Error while sending forgot password email
        this.displayError(error);
      });
    } else {
      return false;
    }
  }

  cancel() {
    this.location.back();
  }

  private sendForgotPasswordEmail() {
    this.forgotPasswordService.sendForgotPwdEmail(this.username).subscribe((res: any) => {
      // forgot password email sent successfully
      this.notify.success('Password link send');
    }, (error) => {
      // Error while sending forgot password email
      this.displayError(error);
    });
  }

  private displayError(error) {
    if (error && error.error && error.error.message) {
      this.notify.error(error.error.message);
    } else {
      this.notify.error('INTERNAL_SERVER_ERROR');
    }
  }
}
