import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { FoldboxListWidgetComponent } from './foldbox-list-widget.component';
import { FoldboxTabsModule } from './foldbox-tabs';
import { FoldboxListModule } from './foldbox-list';

@NgModule({
  imports: [CommonModule, QaEnginePageStructureModule, FoldboxTabsModule, FoldboxListModule],
  declarations: [FoldboxListWidgetComponent],
  exports: [FoldboxListWidgetComponent]
})
export class FoldboxListWidgetModule {
}
