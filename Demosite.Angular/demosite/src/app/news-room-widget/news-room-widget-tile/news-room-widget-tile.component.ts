import { Component, HostBinding, Input, TrackByFunction, ViewEncapsulation } from '@angular/core';
import { Observable } from 'rxjs';
import SwiperCore, { Pagination } from 'swiper';
import { NewsPost, NewsRoomWidgetTileService } from './news-room-widget-tile.service';

export interface NewsCategory {
  id: number;
  alias: string;
  title: string;
  url: string;
}

SwiperCore.use([Pagination]);

@Component({
  selector: 'qa-news-room-widget-tile',
  templateUrl: './news-room-widget-tile.component.html',
  styleUrls: ['./news-room-widget-tile.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [NewsRoomWidgetTileService]
})
export class NewsRoomWidgetTileComponent {
  @Input()
  public set category(category: NewsCategory) {
    this.title = category.title;
    this.url = category.url;
    this.posts$ = this.newsRoomWidgetTileService.getNewsPosts(category.id, category.alias);
  }

  @HostBinding('class') hostCssClasses = 'news__tile';

  public title!: string;
  public url!: string;
  public posts$!: Observable<NewsPost[]>;
  public readonly trackById: TrackByFunction<NewsPost> = (_, item) => item.id;

  constructor(private readonly newsRoomWidgetTileService: NewsRoomWidgetTileService) {
  }
}
