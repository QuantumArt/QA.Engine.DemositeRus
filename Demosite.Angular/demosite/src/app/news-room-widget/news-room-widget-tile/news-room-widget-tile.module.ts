import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterNavigationDirectiveModule } from '@quantumart/qa-engine-page-structure-angular';
import { NewsRoomWidgetTileComponent } from './news-room-widget-tile.component';

@NgModule({
  imports: [CommonModule, RouterNavigationDirectiveModule],
  declarations: [NewsRoomWidgetTileComponent],
  exports: [NewsRoomWidgetTileComponent]
})
export class NewsRoomWidgetTileModule {
}
