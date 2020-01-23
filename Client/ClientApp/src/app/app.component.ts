import { Component, OnInit, Renderer2 } from '@angular/core';
import { Spinkit, SpinnerVisibilityService } from 'ng-http-loader';
import { Subscription } from 'rxjs';
import { Title } from '@angular/platform-browser';
import { Router, NavigationEnd, NavigationStart, NavigationCancel, NavigationError, Event } from '@angular/router';
import { EventService } from './core/services/event-service';
import { EventConstants } from './shared/constants/event-constants';
import { UserService } from './shared/services/user.service';
import { StorageService } from './core/services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'TODOApp';
  spinkit = Spinkit;
  subscription: Subscription = new Subscription();
  constructor(
    private router: Router,
    private renderer: Renderer2,
    private spinner: SpinnerVisibilityService,
    private eventService: EventService,
    private userService: UserService,
    private storageService: StorageService,
    private titleService: Title) {
  }

  ngOnInit() {
    // this.getAllLanguages();

    this.subscription.add(this.router.events.subscribe((event: Event) => {
      if (event instanceof NavigationEnd) {
        const title = this.getTitle(this.router.routerState, this.router.routerState.root).join('-');
        this.titleService.setTitle(title);

        return;
      }
      switch (true) {
        case event instanceof NavigationStart: {
          this.spinner.show();
          break;
        }

        case event instanceof NavigationEnd:
        case event instanceof NavigationCancel:
        case event instanceof NavigationError: {
          this.spinner.hide();
          break;
        }
        default: {
          if (this.spinner.visibility$) {
            this.spinner.hide();
          }
          break;
        }
      }
    }));

    this.router.routeReuseStrategy.shouldReuseRoute = () => {
      return false;
    };
    this.subscription.add(this.eventService.subscribe(EventConstants.ModalOpen, () => {
      this.renderer.addClass(document.body, 'modal-open');
    }));
    this.subscription.add(this.eventService.subscribe(EventConstants.ModalClose, () => {
      this.renderer.removeClass(document.body, 'modal-open');
    }));
  }

  getTitle(state, parent) {
    const data = [];
    if (parent && parent.snapshot.data && parent.snapshot.data.title) {
      data.push(parent.snapshot.data.title);
    }

    if (state && parent) {
      data.push(... this.getTitle(state, state.firstChild(parent)));
    }
    return data;
  }
}
