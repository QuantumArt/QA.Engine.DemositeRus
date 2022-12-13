import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';

export interface TextPageDetails extends WidgetDetails {
  title: string;
  text: string;
}

@Component({
  selector: 'qa-text-page',
  templateUrl: './text-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TextPageComponent {
  public readonly pageDetails$: Observable<TextPageDetails> = this.activatedRoute.data.pipe(
    filter(data => data['details']),
    map(data => data['details'] as TextPageDetails),
  );

  constructor(private readonly activatedRoute: ActivatedRoute) {
  }
}
