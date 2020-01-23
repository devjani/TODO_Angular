import { Injectable } from '@angular/core';

export * from './auth.service';
export * from './http-client.service';
export * from './login.service';
export * from './storage.service';
export * from './notification.service';

@Injectable({ providedIn: 'root' })
export class CoreServices {
}
