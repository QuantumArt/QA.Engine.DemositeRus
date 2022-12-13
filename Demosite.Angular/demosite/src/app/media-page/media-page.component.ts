import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';

export interface MediaPageDetails extends WidgetDetails {
  title: string;
}

@Component({
  selector: 'qa-media-page',
  templateUrl: './media-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MediaPageComponent {
  public readonly pageDetails$: Observable<MediaPageDetails> = this.activatedRoute.data.pipe(
    filter(data => data['details']),
    map(data => data['details'] as MediaPageDetails),
  );

  constructor(private readonly activatedRoute: ActivatedRoute) {
  }
}
