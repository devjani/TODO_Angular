import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { EventConstants } from '../../shared/constants/event-constants';
import { User } from '../../models/user';
import { EventService } from '../../core/services/event-service';
import { UserService } from '../../shared/services/user.service';
import { AuthService, NotificationService } from '../../core/services';
declare var $: any;

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html'
})
export class AuthComponent implements OnInit {
  isCollapsed = true;
  currentLanguage: string;
  loggedInUser: User;
  sideBarClass: string;
  currentYear: number;
  title = '';
  constructor(
    private activatedRoute: ActivatedRoute,
    private eventService: EventService,
    private authService: AuthService,
    private router: Router,
    private userService: UserService,
    private notify: NotificationService
  ) { }

  ngOnInit() {
    this.sideBarClass = '';
    this.loggedInUser = this.authService.getUserInfoAuth();
    this.currentYear = new Date().getFullYear();
    this.eventService.subscribe('CHANGE_HEADER_TITLE', ({ title = '' }) => {
      if (title) {
        this.title = title;
      } else {
        this.title = '';
      }
    });
  }

  toggleSidebar() {
    this.sideBarClass = this.sideBarClass === 'active' ? '' : 'active';
    if (this.sideBarClass) {
      $('.sidebar').css('margin-left', '0');
    } else {
      $('.sidebar').css('margin-left', '-250px');
    }


    // this.sideBarClass = this.sideBarClass === 'active' ? '' : 'active';
    // $(".sidebar").css("margin-left", "0");
  }

  logout() {
    this.authService.logout();
    this.notify.success('Logged out');
    this.router.navigate(['/login']);
  }
}
