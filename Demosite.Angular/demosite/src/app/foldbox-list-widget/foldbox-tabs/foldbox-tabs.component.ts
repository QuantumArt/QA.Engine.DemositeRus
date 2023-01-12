import { ChangeDetectionStrategy, Component, Input, TrackByFunction } from '@angular/core';
import { FoldboxListItem } from '../foldbox-list-widget.service';
import { TabOpenEventData } from '../../behaviors';
import { CardSliderService } from '../../text-page/card-slider.service';

@Component({
  selector: 'qa-foldbox-tabs',
  templateUrl: './foldbox-tabs.component.html',
  styleUrls: ['./foldbox-tabs.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FoldboxTabsComponent {
  @Input() public title?: string | null;
  @Input() public items!: FoldboxListItem[] | null;

  public readonly trackById: TrackByFunction<FoldboxListItem> = (_, item) => item.id;

  constructor(private readonly cardSliderService: CardSliderService) {
  }

  public initializeCardSlider(event: TabOpenEventData): void {
    const sliderContainer = event.bodyElement.querySelector<HTMLElement>('.card-slider')
    this.cardSliderService.initializeSlider(sliderContainer);
  }
}
