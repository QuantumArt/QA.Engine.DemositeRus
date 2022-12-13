import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MediaPageRoutingModule } from './media-page-routing.module';
import { MediaPageComponent } from './media-page.component';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';

@NgModule({
  imports: [CommonModule, MediaPageRoutingModule, QaEnginePageStructureModule],
  declarations: [MediaPageComponent],
})
export class MediaPageModule {
}
