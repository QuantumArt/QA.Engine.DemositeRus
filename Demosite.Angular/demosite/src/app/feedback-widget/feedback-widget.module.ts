import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { FeedbackWidgetComponent } from './feedback-widget.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [CommonModule, ReactiveFormsModule, QaEnginePageStructureModule],
  declarations: [FeedbackWidgetComponent],
  exports: [FeedbackWidgetComponent]
})
export class FeedbackWidgetModule {
}
