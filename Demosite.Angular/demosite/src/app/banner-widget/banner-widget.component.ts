import {
  ChangeDetectionStrategy,
  Component,
  Inject,
  Input,
  PLATFORM_ID,
  TrackByFunction,
  ViewEncapsulation
} from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { Observable } from 'rxjs';
import SwiperCore, { Autoplay, Navigation, Pagination, SwiperOptions } from 'swiper';
import { WidgetComponent, WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';
import { BannerItem, BannerWidgetService } from './banner-widget.service';

const AUTOPLAY_DELAY = 5000;
SwiperCore.use([Autoplay, Pagination, Navigation]);

export interface BannerWidgetDetails extends WidgetDetails {
  swipedelay?: number;
  banneritemids: number[];
}

@Component({
  selector: 'qa-banner-widget',
  templateUrl: './banner-widget.component.html',
  styleUrls: ['./banner-widget.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [BannerWidgetService]
})
export class BannerWidgetComponent implements WidgetComponent {
  @Input()
  public set widget(widget: BannerWidgetDetails) {
    if (widget.swipedelay) {
      this.swipeDelay = widget.swipedelay * 1000;
    }

    if (widget.banneritemids) {
      this.items$ = this.bannerWidgetService.getBanners(widget.banneritemids);
    }
  }

  public readonly isPlatformBrowser = isPlatformBrowser(this.platformId);
  public readonly trackById: TrackByFunction<BannerItem> = (_, item) => item.id;
  public swipeDelay = AUTOPLAY_DELAY;
  public items$?: Observable<BannerItem[]>;
  public swiperConfig: SwiperOptions = {
    enabled: this.isPlatformBrowser,
    autoplay: {
      delay: this.swipeDelay,
      disableOnInteraction: true,
    },
    navigation: {
      nextEl: '.banner-slider__button--next',
      prevEl: '.banner-slider__button--prev'
    },
    pagination: {
      clickable: true,
      el: '.banner-slider__pagination',
    },
    loop: true,
  }

  constructor(
    @Inject(PLATFORM_ID) private readonly platformId: Object,
    private readonly bannerWidgetService: BannerWidgetService
  ) {
  }

  public handleBannerClick(event: Event, bannerUrl?: string): void {
    if (!bannerUrl) {
      event.preventDefault();
    }
  }
}
