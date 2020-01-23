import { NgModule, ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NonAuthLayoutComponent } from './non-auth-layout.component';
import { LoginComponent } from './login/login.component';
import { ForgotPasswordComponent } from './forgot-password/forgot-password.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { SetPasswordComponent } from './set-password/set-password.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'login'
  },
  {
    path: 'login',
    component: LoginComponent,
    data: { title: 'Login | TODO' }
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent,
    data: { title: 'Forgot Password | TODO' }
  },
  {
    path: 'resetpassword',
    component: SetPasswordComponent,
    data: { title: 'Reset Password | TODO' }
  },
  {
    path: 'set-password',
    component: SetPasswordComponent,
    data: { title: 'Set Password | TODO' }
  }
];

export const NonAuthRoutingModule: ModuleWithProviders = RouterModule.forChild(routes);
