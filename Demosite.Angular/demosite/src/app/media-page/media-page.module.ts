import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { SafePipeModule } from '../pipes';
import { FoldboxDirectiveModule } from '../behaviors';
import { BreadcrumbsModule } from '../breadcrumbs';
import { MediaPageRoutingModule } from './media-page-routing.module';
import { MediaPageComponent } from './media-page.component';
import { MediaEventModule } from './media-event';

@NgModule({
  imports: [
    CommonModule,
    QaEnginePageStructureModule,
    MediaPageRoutingModule,
    BreadcrumbsModule,
    SafePipeModule,
    FoldboxDirectiveModule,
    MediaEventModule
  ],
  declarations: [MediaPageComponent],
})
export class MediaPageModule {
}
