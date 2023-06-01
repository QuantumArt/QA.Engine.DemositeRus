import { ChangeDetectionStrategy, Component, ViewEncapsulation } from '@angular/core';
import { NodeDetails } from '@quantumart/qa-engine-page-structure-angular';
import { SiteNodeComponent, SiteNodeService } from '../services';
import { TabOpenEventData } from '../behaviors';
import { CardSliderService } from './card-slider.service';

export interface TextPageDetails extends NodeDetails {
  title: string;
  text: string;
  hidetitle: boolean;
}

@Component({
  selector: 'qa-text-page',
  templateUrl: './text-page.component.html',
  styleUrls: ['./text-page.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SiteNodeService, CardSliderService]
})
export class TextPageComponent implements SiteNodeComponent {
  public get id(): number {
    return this.siteNodeService.getNodeId();
  }

  public readonly pageDetails$ = this.siteNodeService.getDetails<TextPageDetails>();

  constructor(
    private readonly siteNodeService: SiteNodeService,
    private readonly cardSliderService: CardSliderService
  ) {
  }

  public initializeCardSlider(event: TabOpenEventData): void {
    const sliderContainer = event.bodyElement.querySelector<HTMLElement>('.card-slider')
    this.cardSliderService.initializeSlider(sliderContainer);
  }
}
