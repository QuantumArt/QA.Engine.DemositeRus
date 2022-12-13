import { Component, Input, TrackByFunction } from '@angular/core';
import { Observable } from 'rxjs';
import { WidgetComponent, WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';
import { NewsBlock, NewsRoomWidgetService } from './news-room-widget.service';

export interface NewsRoomWidgetDetails extends WidgetDetails {
}

@Component({
  selector: 'qa-news-room-widget',
  templateUrl: './news-room-widget.component.html',
  providers: [NewsRoomWidgetService]
})
export class NewsRoomWidgetComponent implements WidgetComponent {
  @Input()
  public set widget(widget: NewsRoomWidgetDetails) {
    this.title = widget.title;
    this.blocks$ = this.newsRoomWidgetService.getNewsBlocks();
  }

  public title!: string;
  public blocks$!: Observable<NewsBlock[]>;
  public readonly trackById: TrackByFunction<NewsBlock> = (index, item) => item.id;

  constructor(private readonly newsRoomWidgetService: NewsRoomWidgetService) {
  }
}
