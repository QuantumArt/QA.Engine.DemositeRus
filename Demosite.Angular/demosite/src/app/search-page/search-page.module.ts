import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { SearchPageRoutingModule } from './search-page-routing.module';
import { SearchPageComponent } from './search-page.component';

@NgModule({
  imports: [CommonModule, SearchPageRoutingModule, QaEnginePageStructureModule],
  declarations: [SearchPageComponent],
})
export class SearchPageModule {
}
