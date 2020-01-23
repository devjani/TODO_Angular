import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { TodoListComponent } from './todo-list/todo-list.component';

const routes: Routes = [
  {
    path: '',
    component: TodoListComponent,
    data: { title: 'Todo' }
  },
  {
    path: 'todo-list',
    component: TodoListComponent,
    data: { title: 'Todo' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TodoRoutingModule { }
