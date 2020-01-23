import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { Todo } from 'src/app/models/todo';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ErrorMatcher } from 'src/app/shared/helper/error-matcher';
import { BaseModel } from 'src/app/models/baseModel';
import { Subscription } from 'rxjs';
import { MatDialogRef } from '@angular/material';
import { NotificationService } from 'src/app/core/services';
import { EventService } from 'src/app/core/services/event-service';
import { TodoService } from 'src/app/core/services/todo.service';
import { EventConstants } from 'src/app/shared/constants/event-constants';

@Component({
  selector: 'app-todo-add-edit',
  templateUrl: './todo-add-edit.component.html',
  styleUrls: ['./todo-add-edit.component.scss']
})
export class TodoAddEditComponent implements OnInit, OnDestroy {

  todo: Todo;
  todoId: string;
  addEditTodo: FormGroup;
  errorMatcher: ErrorMatcher;
  statusData: Array<BaseModel> = [];
  isAdd = true;
  @Input() todoDetails: any;
  private subscriptions: Subscription = new Subscription();
  constructor(
    public dialogRef: MatDialogRef<TodoAddEditComponent>,
    private notificationService: NotificationService,
    private todoService: TodoService,
    private eventService: EventService
  ) { }

  ngOnInit() {
    this.initializeForm();
    if (this.todoDetails) {
      this.editForm();
    }
  }
  initializeForm() {
    this.errorMatcher = new ErrorMatcher();
    this.addEditTodo = new FormGroup({
      id: new FormControl(''),
      name: new FormControl('', Validators.required),
      desciption: new FormControl('', Validators.required),
    });
  }
  editForm() {
    this.isAdd = false;
    this.todoId = this.todoDetails.id;
    this.addEditTodo.patchValue({
      id: this.todoDetails.id,
      name: this.todoDetails.name.trim(),
      desciption: this.todoDetails.description.trim(),
    });
  }
  get f() { return this.addEditTodo.controls; }
  onSubmit() {
    this.errorMatcher.submitForm();
    if (!this.addEditTodo.invalid) {
      // const userDetails = this.authService.getUserInfoAuth();
      this.todo = new Todo();
      this.todo.TodoId = this.todoId ? this.todoId : '';
      this.todo.Name = this.f.name.value.trim();
      this.todo.Description = this.f.desciption.value.trim();
      this.saveEvent(this.todo);
    }
  }

  async saveEvent(event) {
    this.subscriptions.add(this.todoService.saveTodo(event).subscribe(
      async (res) => {
        if (res) {
          this.dialogRef.close();
          if (this.isAdd) {
            this.notificationService.success('Todo Added');
          } else {
            this.notificationService.success('Todo Updated');
          }

          this.eventService.broadcast(EventConstants.AddUpdateTodo, 0);
        } else {
          this.notificationService.error('Some went wrong');
        }
      }));
  }
  close() {
    this.dialogRef.close();
  }
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }

}
