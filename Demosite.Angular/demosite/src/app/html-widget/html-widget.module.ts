import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DynamicHooksModule } from 'ngx-dynamic-hooks';
import { HtmlWidgetComponent } from './html-widget.component';
import { SafePipeModule } from '../pipes';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';

@NgModule({
  imports: [CommonModule, SafePipeModule, DynamicHooksModule, QaEnginePageStructureModule],
  declarations: [HtmlWidgetComponent],
  exports: [HtmlWidgetComponent],
})
export class HtmlWidgetModule {
}
