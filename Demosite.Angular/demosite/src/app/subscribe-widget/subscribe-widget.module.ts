import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { SubscribeWidgetComponent } from './subscribe-widget.component';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, QaEnginePageStructureModule],
  declarations: [SubscribeWidgetComponent],
  exports: [SubscribeWidgetComponent]
})
export class SubscribeWidgetModule {
}
