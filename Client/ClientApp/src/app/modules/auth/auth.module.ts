import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';

import { SharedModule } from '../../shared/shared.module';


import { CollapseModule } from 'ngx-bootstrap/collapse';

import { AuthComponent } from './auth.component';
import { UserService } from '../../shared/services/user.service';
import { CoreModule } from '../../core/core.module';
import { AuthSharedModule } from './shared/auth-shared.module';

@NgModule({
  declarations: [
    AuthComponent,
  ],
  imports: [
    CommonModule,
    AuthRoutingModule,
    AuthSharedModule,
    SharedModule,
    CoreModule,
    CollapseModule,
  ],
  providers: [
    UserService,
  ]
})
export class AuthModule { }
