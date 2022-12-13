﻿import { Component, Input, TrackByFunction } from '@angular/core';
import { Observable } from 'rxjs';
import { NewsPost, NewsRoomWidgetTileService } from './news-room-widget-tile.service';

export interface NewsCategory {
  id: number;
  alias: string;
  title: string;
  url: string;
}

@Component({
  selector: 'qa-news-room-widget-tile',
  templateUrl: './news-room-widget-tile.component.html',
  providers: [NewsRoomWidgetTileService]
})
export class NewsRoomWidgetTileComponent {
  @Input() public set category(category: NewsCategory) {
    this.title = category.title;
    this.url = category.url;
    this.posts$ = this.newsRoomWidgetTileService.getNewsPosts(category.id, category.alias);
  }

  public title!: string;
  public url!: string;
  public posts$!: Observable<NewsPost[]>;
  public readonly trackById: TrackByFunction<NewsPost> = (_, item) => item.id;

  constructor(private readonly newsRoomWidgetTileService: NewsRoomWidgetTileService) {
  }
}
