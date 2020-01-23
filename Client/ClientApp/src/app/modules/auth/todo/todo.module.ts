import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TodoAddEditComponent } from './todo-add-edit/todo-add-edit.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoService } from 'src/app/core/services/todo.service';
import { TodoRoutingModule } from './todo-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthSharedModule } from '../shared/auth-shared.module';
import { SharedModule } from 'src/app/shared';



@NgModule({
  declarations: [TodoAddEditComponent, TodoListComponent],
  imports: [
    SharedModule,
    TodoRoutingModule,
    CommonModule,
    FormsModule, AuthSharedModule,
    ReactiveFormsModule,
  ],
  providers: [TodoService],
  entryComponents: [TodoAddEditComponent]
})
export class TodoModule { }
