import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { NewsRoomWidgetComponent } from './news-room-widget.component';
import { NewsRoomWidgetTileModule } from './news-room-widget-tile';

@NgModule({
  imports: [CommonModule, QaEnginePageStructureModule, NewsRoomWidgetTileModule],
  declarations: [NewsRoomWidgetComponent],
  exports: [NewsRoomWidgetComponent]
})
export class NewsRoomWidgetModule {
}
