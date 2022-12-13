import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { LayoutModule } from '@angular/cdk/layout';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { TopMenuWidgetComponent } from './top-menu-widget.component';

@NgModule({
  imports: [CommonModule, BrowserAnimationsModule, RouterModule, LayoutModule, QaEnginePageStructureModule],
  declarations: [TopMenuWidgetComponent],
  exports: [TopMenuWidgetComponent]
})
export class TopMenuWidgetModule {
}
