import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MediaPageRoutingModule } from './media-page-routing.module';
import { MediaPageComponent } from './media-page.component';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { BreadcrumbsModule } from '../breadcrumbs/breadcrumbs.module';
import { SafePipeModule } from '../pipes';
import { FoldboxDirectiveModule } from '../behaviors';

@NgModule({
  imports: [CommonModule, MediaPageRoutingModule, QaEnginePageStructureModule, BreadcrumbsModule, SafePipeModule, FoldboxDirectiveModule],
  declarations: [MediaPageComponent],
})
export class MediaPageModule {
}
