import { Injectable } from '@angular/core';

import { MatDialog } from '@angular/material';
import { EventService } from './event-service';
import { ConfirmationDialogComponent } from '../../shared/components/confirmation-dialog/confirmation-dialog.component';
import { EventConstants } from '../../shared/constants/event-constants';



@Injectable()
export class ConfirmationDialogService {

    constructor(
        public dialog: MatDialog,
        private eventService: EventService
    ) { }

    public async confirm(
        title: string,
        message: string,
        btnOkText: string,
        btnCancelText: string,
        widthPx: string = '500px',
        heightPx: string = '225px') {
        const modalRef = this.dialog.open(ConfirmationDialogComponent, {
            width: widthPx,
            height: heightPx
        });
        modalRef.afterOpened().subscribe(() => {
            this.eventService.broadcast(EventConstants.ModalOpen, true);
        });
        modalRef.afterClosed().subscribe(() => {
            this.eventService.broadcast(EventConstants.ModalClose, true);
        });
        modalRef.componentInstance.title = title;
        modalRef.componentInstance.message = message;
        modalRef.componentInstance.btnOkText = btnOkText;
        modalRef.componentInstance.btnCancelText = btnCancelText;
        return modalRef;
    }

}
