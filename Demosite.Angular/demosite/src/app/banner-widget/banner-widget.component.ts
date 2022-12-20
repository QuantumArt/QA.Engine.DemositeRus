import {
  ChangeDetectionStrategy,
  Component,
  Inject,
  Input,
  PLATFORM_ID,
  TrackByFunction,
  ViewEncapsulation
} from '@angular/core';
import { Observable } from 'rxjs';
import SwiperCore, { Autoplay, Pagination } from 'swiper';
import { WidgetComponent, WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';
import { BannerItem, BannerWidgetService } from './banner-widget.service';
import { isPlatformBrowser } from '@angular/common';

SwiperCore.use([Autoplay, Pagination]);

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
  public swipeDelay = 2000;
  public items$?: Observable<BannerItem[]>;

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
