import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { NgForm, FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { LoginViewModel } from '../../../shared/models/login-view-model';
import { LoginService, NotificationService, AuthService } from '../../../core/services';
import { LoggedInUserDetails } from '../../../models/user';
import { AppConstants } from '../../../shared/constants/app-constants';
import { ignoreElements } from 'rxjs/operators';
import { MessageConstants } from 'src/app/shared/message-constants';
import { BaseModel } from 'src/app/models/baseModel';
import { InputConstants } from 'src/app/shared/constants/input-constants';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  loginModel: LoginViewModel;
  emailRequiredErrorText: string;
  passwordRequiredErrorText: string;
  isFormSubmitted: boolean;
  inputConstants = InputConstants;
  userName: string;
  password: string;
  @ViewChild('loginForm', null) loginForm: NgForm;
  private subscriptions: Subscription = new Subscription();
  emailPattern = AppConstants.emailPattern;
  passwordPattern = AppConstants.passwordPattern;
  emailFormatValidationText: string;
  passwordPatternErrorText: string;
  languageData: Array<BaseModel> = [];
  isCustomerLogin = false;

  constructor(
    private loginService: LoginService,
    private notify: NotificationService,
    private authService: AuthService,
    private router: Router,
    private fb: FormBuilder
  ) { }

  ngOnInit() {
    this.loginModel = {
      Username: '',
      Password: '',
      RememberMe: false
    };
    this.emailFormatValidationText = 'EmailPatternError';
    this.emailRequiredErrorText = 'REQUIRED_EMAIL';
    this.passwordRequiredErrorText = 'REQUIRED_PASSWORD';
    this.passwordPatternErrorText = 'PASSWORD_VALIDATION_ERROR';
    this.isFormSubmitted = false;
  }

  async onSubmit(form) {
    this.isFormSubmitted = true;
    if (this.loginForm.valid) {
      this.subscriptions.add(this.loginService.login(this.loginModel).subscribe(async (res: any) => {
        if (res) {
          const userData: any = res;
          this.authService.setAuhToken(userData.token);
          this.authService.setUserInfoAuth(
            this.authService.createUserInfo(userData)
          );
          if (this.loginModel.RememberMe) {
            this.authService.setRememberUser(true);
            this.authService.setRefreshToken(userData.refreshToken);
          } else {
            this.authService.setRememberUser(false);
          }
          this.router.navigateByUrl('/todos');
        } else if (res.message) {
          this.notify.error(res.message);
        }
      }, async (error) => {
        if (error.status === MessageConstants.status_403) {
          this.notify.error('UnauthorizedError');
        } else {
          this.notify.error('INTERNAL_SERVER_ERROR');
        }

      }));
    } else {
      window.scrollTo(0, 0);
      //  this.notify.error(await this.languageService.getTranslatedValue('FORM_VALIDATION_ERROR'));
    }
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }
}
