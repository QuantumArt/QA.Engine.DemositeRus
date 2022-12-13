import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';

export interface NewsPageDetails extends WidgetDetails {
  title: string;
}

@Component({
  selector: 'qa-news-page',
  templateUrl: './news-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NewsPageComponent {
  public readonly pageDetails$: Observable<NewsPageDetails> = this.activatedRoute.data.pipe(
    filter(data => data['details']),
    map(data => data['details'] as NewsPageDetails),
  );

  constructor(private readonly activatedRoute: ActivatedRoute) {
  }
}
