import { Component, OnInit } from '@angular/core';
import { EventService } from '../../../core/services/event-service';
import { EventConstants } from '../../../shared/constants/event-constants';
import { TitleService } from '../../../title.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services';
import { RoleType } from 'src/app/models/common';



@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

  isExpanded = false;
  title = '';
  userName: any;
  showLogout = false;
  userInfo: any;
  isCustomer = false;
  isMobile = localStorage.getItem('is-mobile-device');
  constructor(
    private eventService: EventService,
    public tileService: TitleService, public router: Router, private authService: AuthService
  ) { }

  ngOnInit() {
    this.userInfo = this.authService.getUserInfoAuth();
    this.userName = this.userInfo.name;
    this.isCustomer = this.authService.isCustomerLogin();
  }

  onToggleClick() {
    this.eventService.broadcast(EventConstants.ToggleSidebar, 0);
  }

  changePasswordClick() {
    this.router.navigateByUrl('/change-password');
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/']);

  }
}
