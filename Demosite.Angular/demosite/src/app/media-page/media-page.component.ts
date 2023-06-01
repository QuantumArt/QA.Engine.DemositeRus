import { ChangeDetectionStrategy, Component, TrackByFunction } from '@angular/core';
import { map } from 'rxjs/operators';
import { NodeDetails } from '@quantumart/qa-engine-page-structure-angular';
import { SiteNodeComponent, SiteNodeService } from '../services';
import { MediaEvent, MediaPageService } from './media-page.service';

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
  public readonly trackById: TrackByFunction<MediaEvent> = (_, item) => item.id;

  public readonly firstDay$ = this.mediaPageService.getEvents().pipe(
    map(events => events?.length ? events[0] : null)
  );

  public readonly prevDays$ = this.mediaPageService.getEvents().pipe(
    map(events => events?.length ? events.slice(1) : [])
  );

  constructor(
    private readonly siteNodeService: SiteNodeService,
    private readonly mediaPageService: MediaPageService
  ) {
  }
}
