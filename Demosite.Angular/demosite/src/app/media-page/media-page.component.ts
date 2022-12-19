import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { NodeDetails } from '@quantumart/qa-engine-page-structure-angular';
import { SiteNodeComponent, SiteNodeService } from '../services';
import { MediaPageService } from './media-page.service';

export interface MediaPageDetails extends NodeDetails {
  title: string;
}

@Component({
  selector: 'qa-media-page',
  templateUrl: './media-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SiteNodeService, MediaPageService]
})
export class MediaPageComponent implements SiteNodeComponent {
  public get id(): number {
    return this.siteNodeService.getNodeId();
  }

  public readonly pageDetails$ = this.siteNodeService.getDetails<MediaPageDetails>();

  public readonly firstDay$ = this.mediaPageService.getEvents().pipe(
    map(events => events?.length ? events[0] : null)
  );

  public readonly prevDays$ = this.mediaPageService.getEvents().pipe(
    map(events => events?.length ? events.slice(1) : [])
  );

  constructor(
    private readonly siteNodeService: SiteNodeService,
    private readonly activatedRoute: ActivatedRoute,
    private readonly mediaPageService: MediaPageService
  ) {
  }
}
