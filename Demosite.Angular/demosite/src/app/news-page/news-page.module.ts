import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewsPageRoutingModule } from './news-page-routing.module';
import { NewsPageComponent } from './news-page.component';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';

@NgModule({
  imports: [CommonModule, NewsPageRoutingModule, QaEnginePageStructureModule],
  declarations: [NewsPageComponent],
})
export class NewsPageModule {
}
