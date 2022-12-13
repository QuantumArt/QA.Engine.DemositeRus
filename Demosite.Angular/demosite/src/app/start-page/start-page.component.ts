import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map } from 'rxjs/operators';
import { WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';

export interface StartPageDetails extends WidgetDetails {
  title: string;
}

@Component({
  selector: 'qa-start-page',
  templateUrl: './start-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StartPageComponent {
  public readonly pageDetails$ = this.activatedRoute.data.pipe(
    filter(data => data['details']),
    map(data => data['details'] as StartPageDetails),
  );

  constructor(private readonly activatedRoute: ActivatedRoute) {
  }
}
