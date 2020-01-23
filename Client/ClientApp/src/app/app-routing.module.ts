import { ModuleWithProviders } from '@angular/compiler/src/core';
import { Routes, RouterModule } from '@angular/router';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { AuthGuard, SkipLoginGuard } from './core/guards';
import { LayoutComponent } from './modules/layout/layout/layout.component';

const routes: Routes = [
  {
    path: '',
    canActivate: [SkipLoginGuard],
    loadChildren: () => import(`./modules/non-auth`).then(m => m.NonAuthModule)
  },
  {
    path: '',
    canActivate: [AuthGuard],
    component: LayoutComponent,
    loadChildren: () => import(`./modules/auth`).then(m => m.AuthModule)
  },
  {
    component: NotFoundComponent,
    path: '404',
    data: { title: 'Not Found' }
  },
  {
    path: '**',
    redirectTo: '404',
    data: { title: 'Not Found' }
  }
];

export const AppRoutingModule: ModuleWithProviders = RouterModule.forRoot(routes, {
  onSameUrlNavigation: 'reload'
});
