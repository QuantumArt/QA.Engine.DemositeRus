import { Component } from '@angular/core';
import { Subject } from 'rxjs';
import { SiteNodeComponent } from './services';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public readonly nodeId$ = new Subject<number>();

  public updateCurrentNode(event: SiteNodeComponent): void {
    if (event.id) {
      this.nodeId$.next(event.id);
    }
  }
}
