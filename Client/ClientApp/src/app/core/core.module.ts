import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AuthGuard, SkipLoginGuard } from './guards';
import { ResponseInterceptor } from './interceptors';
import { AuthService, HttpClientService} from './services';
import { NotFoundComponent } from '../shared/components/not-found/not-found.component';
import { MaterialModule } from '../shared/material.module';
import { TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { StorageService } from './services/storage.service';
import { NotificationService } from './services/notification.service';
import { ConfirmationDialogComponent } from '../shared/components/confirmation-dialog/confirmation-dialog.component';
import { ToastrModule } from 'ngx-toastr';
import { AngularFontAwesomeModule } from 'angular-font-awesome';

export function createTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http, '../../assets/i18n/', '.json');
}

@NgModule({
  declarations: [
    NotFoundComponent,
    ConfirmationDialogComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ResponseInterceptor, multi: true },
    AuthGuard,
    SkipLoginGuard,
    AuthService,
    HttpClientService,
    NotificationService,
    StorageService
  ],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    TranslateModule,
    ToastrModule.forRoot({
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    }),
    AngularFontAwesomeModule
  ],
  exports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    TranslateModule,
    ToastrModule
  ],
  entryComponents: [
    ConfirmationDialogComponent
  ]
})
export class CoreModule {
}
