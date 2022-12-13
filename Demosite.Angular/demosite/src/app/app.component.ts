import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { ActivatedRoute, ActivatedRouteSnapshot } from '@angular/router';

interface RouteOutletActivateEvent {
  activatedRoute?: ActivatedRoute;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public title = 'demosite';
  public readonly nodeId$ = new Subject<number>();

  private readonly activatedRoute$ = new Subject<ActivatedRouteSnapshot>();

  public updateCurrentNode(event: RouteOutletActivateEvent): void {
    if (!event.activatedRoute) {
      return;
    }

    const nodeId = event.activatedRoute.snapshot.data['nodeId'] as number;
    if (!nodeId) {
      return;
    }

    this.nodeId$.next(nodeId);
    this.activatedRoute$.next(event.activatedRoute.snapshot);
  }
}
