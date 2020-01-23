import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { CoreModule } from '../core/core.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { NoWhitespaceDirective } from './directives/no-whitespace.directive';
import { MatButtonToggleGroup, MatButtonToggleModule } from '@angular/material';
import { TextBoxComponent } from './components/textbox-component/text-box.component';

@NgModule({
  declarations: [
    ChangePasswordComponent,
    NoWhitespaceDirective,
    TextBoxComponent
  ],
  imports: [
    CoreModule,
    ReactiveFormsModule,
    MatButtonToggleModule
  ],
  exports: [
    CoreModule,
    ReactiveFormsModule,
    MatButtonToggleModule,
    ChangePasswordComponent,
    TextBoxComponent,
    /** Directives  */
    NoWhitespaceDirective,
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ],
  entryComponents: [],
  providers: []
})
export class SharedModule { }
