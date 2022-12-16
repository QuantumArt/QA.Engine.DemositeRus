import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';
import { MediaPageService } from './media-page.service';

export interface MediaPageDetails extends WidgetDetails {
  title: string;
}

@Component({
  selector: 'qa-media-page',
  templateUrl: './media-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [MediaPageService]
})
export class MediaPageComponent {
  public readonly pageDetails$: Observable<MediaPageDetails> = this.activatedRoute.data.pipe(
    filter(data => data['details']),
    map(data => data['details'] as MediaPageDetails),
  );

  public firstDay$ = this.mediaPageService.getEvents().pipe(map(events => events?.length ? events[0] : null));
  public prevDays$ = this.mediaPageService.getEvents().pipe(map(events => events?.length ? events.slice(1) : []));

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly mediaPageService: MediaPageService
  ) {
  }
}
