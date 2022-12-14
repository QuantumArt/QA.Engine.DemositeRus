import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { SafePipeModule } from '../pipes';
import { DialogDirectiveModule, FoldboxDirectiveModule } from '../behaviors';
import { TextPageRoutingModule } from './text-page-routing.module';
import { TextPageComponent } from './text-page.component';
import { BreadcrumbsModule } from '../breadcrumbs/breadcrumbs.module';

@NgModule({
  imports: [
    CommonModule,
    TextPageRoutingModule,
    QaEnginePageStructureModule,
    SafePipeModule,
    FoldboxDirectiveModule,
    BreadcrumbsModule,
    DialogDirectiveModule
  ],
  declarations: [TextPageComponent],
})
export class TextPageModule {
}
