import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { StartPageRoutingModule } from './start-page-routing.module';
import { StartPageComponent } from './start-page.component';

@NgModule({
  imports: [CommonModule, StartPageRoutingModule, QaEnginePageStructureModule],
  declarations: [StartPageComponent],
})
export class StartPageModule {
}
