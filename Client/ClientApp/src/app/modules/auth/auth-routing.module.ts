import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ChangePasswordComponent } from '../../shared/components/change-password/change-password.component';
import { TodoListComponent } from './todo/todo-list/todo-list.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'todos',
    pathMatch: 'full'
  },
  {
    path: 'todos',
    loadChildren: () => import(`./todo/todo.module`).then(m => m.TodoModule),
  },
  {
    path: 'change-password',
    component: ChangePasswordComponent,
    data: { title: 'change password' }
  }
];

export const AuthRoutingModule: ModuleWithProviders = RouterModule.forChild(routes);

