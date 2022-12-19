import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NewsPageRoutingModule } from './news-page-routing.module';
import { NewsPageComponent } from './news-page.component';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { BreadcrumbsModule } from '../breadcrumbs';
import { NewsListModule } from './news-list';
import { NewsDetailsModule } from './news-details';

@NgModule({
  imports: [
    CommonModule,
    NewsPageRoutingModule,
    QaEnginePageStructureModule,
    BreadcrumbsModule,
    NewsListModule,
    NewsDetailsModule
  ],
  declarations: [NewsPageComponent],
})
export class NewsPageModule {
}
