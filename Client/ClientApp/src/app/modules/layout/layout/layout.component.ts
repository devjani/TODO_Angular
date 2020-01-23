import { Component, OnInit, OnDestroy } from '@angular/core';
import { EventService } from 'src/app/core/services/event-service';
import { EventConstants } from 'src/app/shared/constants/event-constants';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit , OnDestroy {

  setIndex = false;
  private subscriptions: Subscription = new Subscription();
  constructor(private eventService: EventService) { }

  ngOnInit() {
    this.subscriptions.add(this.eventService.subscribe(EventConstants.RefreshZIndex , (value) => this.setIndex = value ));
  }

  ngOnDestroy() {
    this.subscriptions.unsubscribe();
  }

}
