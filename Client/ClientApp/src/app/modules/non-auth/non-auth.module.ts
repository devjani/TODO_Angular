import { NgModule } from '@angular/core';

import { NonAuthRoutingModule } from './non-auth-routing.module';
import { NonAuthLayoutComponent } from './non-auth-layout.component';

import { LoginComponent } from './login/login.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LandingPageComponent } from './landing-page/landing-page.component';

import { SetPasswordComponent } from './set-password/set-password.component';
import { UserService } from '../../core/services/user.service';
import { ForgotPasswordService } from '../../core/services/forgot-password.service';
import { SharedModule } from '../../shared';
import { LoginService } from '../../core/services';



@NgModule({
  declarations: [
    NonAuthLayoutComponent,
    LandingPageComponent,
    LoginComponent,
    ForgotPasswordComponent,
    SetPasswordComponent
  ],
  imports: [
    NonAuthRoutingModule,
    SharedModule
  ],
  providers: [
    LoginService,
    UserService,
    ForgotPasswordService
  ]
})
export class NonAuthModule { }
