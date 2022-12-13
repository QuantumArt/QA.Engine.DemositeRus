import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SwiperModule } from 'swiper/angular';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { BannerWidgetComponent } from './banner-widget.component';

@NgModule({
  imports: [CommonModule, SwiperModule, QaEnginePageStructureModule],
  declarations: [BannerWidgetComponent],
  exports: [BannerWidgetComponent]
})
export class BannerWidgetModule {
}
