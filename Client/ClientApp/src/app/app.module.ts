import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoreModule } from './core/core.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import localeFr from '@angular/common/locales/fr';
import localeNL from '@angular/common/locales/nl';
import { registerLocaleData } from '@angular/common';
import { NgHttpLoaderModule, SpinnerVisibilityService } from 'ng-http-loader';
import { EventService } from './core/services/event-service';
import { ConfirmationDialogService } from './core/services/confirm-dialog.service';
import { UserService } from './shared/services/user.service';
import { LayoutComponent } from './modules/layout/layout/layout.component';
import { HeaderComponent } from './modules/layout/header/header.component';
import { SideNavBarComponent } from './modules/layout/side-nav-bar/side-nav-bar.component';
import { LoginService, AuthService } from './core/services';

registerLocaleData(localeFr);
registerLocaleData(localeNL);
interface JavaScriptInterface {
  onSessionTimeout(): any;
  updateToken(authToken: string, refreshToken: string): any;
}

declare var JavaScriptInterface: JavaScriptInterface;

@NgModule({
  declarations: [
    AppComponent,
    LayoutComponent, HeaderComponent, SideNavBarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CoreModule,
    HttpClientModule,
    NgHttpLoaderModule.forRoot(),

  ],
  providers: [
    EventService,
    ConfirmationDialogService,
    LoginService,
    UserService,
    SpinnerVisibilityService,
    {
      provide: APP_INITIALIZER,
      useFactory: (
        authService: AuthService, loginService: LoginService,
        spinner: SpinnerVisibilityService) =>
        async () => {
          try {
          const isValidToken = await authService.validateToken(localStorage.getItem('auth-token')).toPromise();
          if (!isValidToken && JSON.parse(localStorage.getItem('remember-user')) === false ) {
            authService.logout();
            return [];
          }
          if (JSON.parse(localStorage.getItem('remember-user')) === true && localStorage.getItem('refresh-token')
              && !isValidToken) {
            const response = await authService.refreshToken().toPromise();
            authService.setAuhToken(response.token);
            authService.setRefreshToken(response.refreshToken);
          }
        } catch {
          return [];
        }
        },
      deps: [AuthService, LoginService, SpinnerVisibilityService],
      multi: true
    }
  ],
  bootstrap: [
    AppComponent
  ],
  exports: [
    HttpClientModule
  ]
})
export class AppModule { }
