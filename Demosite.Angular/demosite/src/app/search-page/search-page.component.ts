import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';

export interface SearchPageDetails extends WidgetDetails {
  title: string;
}

@Component({
  selector: 'qa-search-page',
  templateUrl: './search-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SearchPageComponent {
  public readonly pageDetails$: Observable<SearchPageDetails> = this.activatedRoute.data.pipe(
    filter(data => data['details']),
    map(data => data['details'] as SearchPageDetails),
  );

  constructor(private readonly activatedRoute: ActivatedRoute) {
  }
}
