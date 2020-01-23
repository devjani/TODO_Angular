import { Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { TodoService } from 'src/app/core/services/todo.service';
import { MatPaginator, MatSort, MatDialog, MatSortable, MatTableDataSource } from '@angular/material';
import { Subscription } from 'rxjs';
import { BaseModel } from 'src/app/models/baseModel';
import { Router } from '@angular/router';
import { EventService } from 'src/app/core/services/event-service';
import { NotificationService } from 'src/app/core/services';
import { TitleService } from 'src/app/title.service';
import { EventConstants } from 'src/app/shared/constants/event-constants';
import { PaginationRequest } from 'src/app/models/paginationRequest';
import { TodoAddEditComponent } from '../todo-add-edit/todo-add-edit.component';
import { ConfirmationDialogComponent } from 'src/app/shared/components/confirmation-dialog/confirmation-dialog.component';
import { MessageConstants } from 'src/app/shared/message-constants';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss']
})
export class TodoListComponent implements OnInit, OnDestroy {
  columns = ['name', 'actions'];
  isLoading = false;
  show = false;
  pageSize = 10;
  todoId = null;
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  private subscriptions: Subscription = new Subscription();
  totalMasterCount: any;
  dataSource: any;
  searchFilter: boolean;
  statusData: Array<BaseModel> = [];
  constructor(
    private todoService: TodoService,
    private router: Router,
    private dialog: MatDialog,
    private eventService: EventService,
    private notificationService: NotificationService,
    private titleService: TitleService) {
      this.titleService.headerTitle = 'Todo Management';
      this.titleService.backButton = false;
  }

  ngOnInit() {
    this.getTodoListing();

    this.eventService.subscribe(EventConstants.AddUpdateTodo, () => {
      this.getTodoListing();
    });
    this.sort.sort({
      id: 'name',
      start: 'asc'
    } as MatSortable);
  }

  filterData() {
    this.getTodoListing();
  }

  getTodoListing() {
    this.isLoading = true;
    let pageNo: number;
    let pageSize: number;
    if (this.paginator && (this.paginator.pageIndex || this.paginator.pageSize)) {
      pageNo = (this.paginator.pageIndex + 1);
      pageSize = this.paginator.pageSize;
    } else {
      pageNo = 1;
      pageSize = 10;
    }
    const paginationRequest = new PaginationRequest();
    paginationRequest.pageNo = pageNo;
    paginationRequest.pageSize = pageSize;
    paginationRequest.sortBy = (this.sort && this.sort.active) ? this.sort.active : '';
    paginationRequest.sortOrder = (this.sort && this.sort.direction) ? this.sort.direction === 'asc' ? 1 : 2 : 1;
    this.subscriptions.add(this.todoService.getTodoListing(paginationRequest)
      .subscribe((data: any) => {
        if (data && data.data) {
          this.totalMasterCount = data.totalRecords;
          this.show = data.totalRecords > 10;
        }
        this.dataSource = new MatTableDataSource(data.data);
        this.isLoading = false;
      }, error => {
        this.isLoading = false;
      }));
  }
  sortData() {
    this.getTodoListing();
  }

  addEditTodo(todoDetails) {
    const dialogRef = this.dialog.open(TodoAddEditComponent, {
      width: '700px',
      height: '620px'
    });
    dialogRef.componentInstance.todoDetails = todoDetails;
    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        console.log(res);
      }
    });
  }
  async deleteTodo(todoId) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      width: '700px',
      height: '620px'
    });
    dialogRef.componentInstance.message = 'Are you sure you wan to delete this todo?';
    dialogRef.componentInstance.title = 'Delete Todo';
    dialogRef.componentInstance.btnCancelText = 'Cancel';
    dialogRef.componentInstance.btnOkText = 'Delete';
    this.todoId = todoId;

    dialogRef.componentInstance.accept = () => {
      this.confirmDelete(todoId);
    };

    dialogRef.afterClosed().subscribe(res => {
      if (res) {
        console.log(res);
      }
    });
  }

  confirmDelete(roleId) {
    this.subscriptions.add(this.todoService.deleteTodo(roleId).subscribe(
      (res) => {
        if (res) {
          this.dialog.closeAll();
          this.notificationService.success(MessageConstants.deleteNote);
          this.eventService.broadcast(EventConstants.AddUpdateTodo, 0);
        }
      }));
  }
  onMatTableRowClick(row) {
    this.subscriptions.add(this.todoService.getTodoById(row.id).subscribe(
      (res) => {
        if (res) {
          const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
            width: '700px',
            height: '620px'
          });
          dialogRef.componentInstance.message = res.description;
          dialogRef.componentInstance.title = res.name;
          dialogRef.componentInstance.btnCancelText = 'Close';
          dialogRef.componentInstance.btnOkText = 'Ok';
        }
      }));
  }
  ngOnDestroy(): void {
    this.subscriptions.unsubscribe();
  }
}
