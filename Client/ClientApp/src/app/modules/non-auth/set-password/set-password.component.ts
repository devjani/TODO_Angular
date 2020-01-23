import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AppConstants } from '../../../shared/constants/app-constants';
import { NotificationService } from '../../../core/services';
import { UserService } from '../../../shared/services/user.service';
import { ForgotPasswordModel } from '../../../shared/models/forgot-password-model';

@Component({
  selector: 'app-set-password',
  templateUrl: './set-password.component.html',
  styleUrls: ['./set-password.component.scss']
})
export class SetPasswordComponent implements OnInit, OnDestroy {
  setPasswordModel: ForgotPasswordModel;
  @ViewChild('setPasswordForm', null) setPasswordForm: NgForm;
  private subscriptions: Subscription = new Subscription();
  isFormSubmitted: boolean;
  passwordRequiredErrorText: string;
  confirmPasswordRequiredErrorText: string;
  passwordPatternErrorText = 'PasswordPatternError';
  passwordPattern = AppConstants.passwordPattern;
  Password = 'Password';
  constructor(
    private notify: NotificationService,
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit() {
    this.setPasswordModel = new ForgotPasswordModel();
    this.getToken();
    this.isFormSubmitted = false;
    this.passwordRequiredErrorText = 'NEW_PASSWORD_REQUIRED';
    this.confirmPasswordRequiredErrorText = 'REQUIRED_CONFIRM_PASSWORD';
  }

  private getToken() {
    this.route.queryParams.subscribe(
      (param) => {
        this.setPasswordModel.Token = param.code.toString();
        this.validateToken();
      }
    );
  }

  setPassword() {
    this.isFormSubmitted = true;
    if (this.setPasswordForm.valid
      && this.setPasswordModel.Password === this.setPasswordModel.ConfirmPasswrod) {
      this.subscriptions.add(this.userService.setPassword(this.setPasswordModel)
        .subscribe((res: any) => {
          if (res.result) {
            this.notify.success('Password successfully reset');
            this.router.navigate(['/login']);
          }
        }, () => {
          this.notify.error('INTERNAL_SERVER_ERROR');
        }));
    }
  }

  private validateToken() {
    this.subscriptions.add(this.userService.validateResetPasswordToken(this.setPasswordModel.Token)
      .subscribe((res: any) => {
        if (!res) {
          this.notify.error(res.message);
          this.router.navigate(['/login']);
        } else if (!res && !res.result) {
          this.notify.error('Password reset link expired');
          this.router.navigate(['/forgot-password']);
        }
      }, (error) => {
        this.notify.error('INTERNAL_SERVER_ERROR');
      }));
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
