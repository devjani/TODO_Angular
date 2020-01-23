import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';

import { MatDrawer } from '@angular/material';
import { Subscription } from 'rxjs';
import { AuthService } from '../../../core/services';
import { Router } from '@angular/router';
import { EventService } from '../../../core/services/event-service';
import { EventConstants } from '../../../shared/constants/event-constants';
import { RoleType } from 'src/app/models/common';

@Component({
  selector: 'app-side-nav-bar',
  templateUrl: './side-nav-bar.component.html',
  styleUrls: ['./side-nav-bar.component.scss']
})

export class SideNavBarComponent implements OnInit, OnDestroy {

  isExpanded = false;
  private subscription: Subscription = new Subscription();
  userInfo: any;
  @ViewChild(MatDrawer, { static: false }) sideNav: MatDrawer;
  isCustomer = false;
  constructor(
    private eventService: EventService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.subscription.add(this.eventService.subscribe(EventConstants.ToggleSidebar, () => {
      this.sideNav.toggle();
    }));
    this.userInfo = this.authService.getUserInfoAuth();
    this.isCustomer = this.authService.isCustomerLogin();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
