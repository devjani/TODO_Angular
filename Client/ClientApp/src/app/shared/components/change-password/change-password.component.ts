import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { User } from '../../../models/user';
import { AppConstants } from '../../constants/app-constants';
import { UserService } from '../../services/user.service';
import { NotificationService, AuthService } from '../../../core/services';
import { EventService } from '../../../core/services/event-service';
import { ChangePasswordModel } from '../../models/change-password-model';
import { ErrorMatcher } from '../../helper/error-matcher';
import { TitleService } from 'src/app/title.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.scss']
})
export class ChangePasswordComponent implements OnInit, OnDestroy {
  userDetails: User;
  changePasswordForm: FormGroup;
  private subscriptions: Subscription = new Subscription();
  isFormSubmitted: boolean;
  passwordRequiredErrorText: string;
  passwordPatternErrorText = 'PasswordPatternError';
  newPasswordRequiredErrorText: string;
  changePasswordModel: ChangePasswordModel = new ChangePasswordModel();
  errorMatcher: ErrorMatcher;
  constructor(
    private userService: UserService,
    private notify: NotificationService,
    private authService: AuthService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private eventService: EventService,
    private tileService: TitleService,
    private fb: FormBuilder
  ) {
      this.tileService.headerTitle = 'ChangePassword';
      this.tileService.backButton = false;
  }

  ngOnInit() {
    this.passwordRequiredErrorText = 'PASSWORD_REQUIRED';
    this.newPasswordRequiredErrorText = 'NEW_PASSWORD_REQUIRED';
    this.isFormSubmitted = false;
    this.userDetails = this.authService.getUserInfoAuth();
    this.initializeForm();
  }
  initializeForm() {
    this.changePasswordForm = this.fb.group({
      oldPassword: new FormControl('', Validators.required),
      newPassword: new FormControl('', [Validators.required, Validators.pattern(AppConstants.passwordPattern)]),
      confirmPassword: new FormControl('', Validators.required)
    });

  }

  async onSubmit() {
    this.isFormSubmitted = true;
    if (this.changePasswordForm.valid &&
      (this.f.oldPassword.value.trim() !== this.f.newPassword.value.trim())) {
      this.changePasswordModel.OldPassword = this.f.oldPassword.value.trim();
      this.changePasswordModel.NewPassword = this.f.newPassword.value.trim();
      this.changePasswordModel.ConfirmNewPassword = this.f.confirmPassword.value.trim();
      this.changePasswordModel.UserId = this.userDetails.Id;
      this.subscriptions.add(this.userService.changePassword(this.changePasswordModel).subscribe(async (res: any) => {
        this.isFormSubmitted = false;
        if (res.result) {
          this.changePasswordModel = new ChangePasswordModel();
          this.notify.success('ChangePasswordSuccess');
          this.router.navigate(['/dashboard']);
        } else {
          this.notify.error('UnableChangePassword');
        }
      }, (error) => {
        this.displayError(error);
      }));
    } else {
      this.displayError('FORM_VALIDATION_ERROR');
    }
  }

  get f() { return this.changePasswordForm.controls; }

  async displayError(error) {
    if (error && error.error && error.error.message) {
      this.notify.error(error.error.message);
    } else {
      this.notify.error('INTERNAL_SERVER_ERROR');
    }
  }

  cancel() {
    // this.location.back();
    this.router.navigate(['/dashboard']);
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

}
