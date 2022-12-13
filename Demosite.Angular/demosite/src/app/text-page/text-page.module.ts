import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { SafePipeModule } from '../pipes';
import { TextPageRoutingModule } from './text-page-routing.module';
import { TextPageComponent } from './text-page.component';

@NgModule({
  imports: [CommonModule, TextPageRoutingModule, QaEnginePageStructureModule, SafePipeModule],
  declarations: [TextPageComponent],
})
export class TextPageModule {
}
