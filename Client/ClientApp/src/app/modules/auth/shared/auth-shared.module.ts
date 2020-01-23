import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';
import { UserService } from 'src/app/core/services/user.service';
import { SharedModule } from 'src/app/shared/shared.module';

@NgModule({
    imports: [
        SharedModule
    ],
    exports: [
    ],
    declarations: [
    ],
    providers: [
        AuthService,
        UserService
    ],
    entryComponents: [] ,
    schemas: [
        CUSTOM_ELEMENTS_SCHEMA
    ],
})

export class AuthSharedModule { }
