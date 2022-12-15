import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';
import { TabOpenEventData } from '../behaviors/tabs/tabs.directive';
import { CardSliderService } from './card-slider.service';

export interface TextPageDetails extends WidgetDetails {
  title: string;
  text: string;
  hidetitle: boolean;
}

@Component({
  selector: 'qa-text-page',
  templateUrl: './text-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [CardSliderService]
})
export class TextPageComponent {
  public readonly pageDetails$: Observable<TextPageDetails> = this.activatedRoute.data.pipe(
    filter(data => data['details']),
    map(data => data['details'] as TextPageDetails),
  );

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly cardSliderService: CardSliderService
  ) {
  }

  public initializeCardSlider(event: TabOpenEventData): void {
    const sliderContainer = event.bodyElement.querySelector<HTMLElement>('.card-slider')
    this.cardSliderService.initializeSlider(sliderContainer);
  }
}
