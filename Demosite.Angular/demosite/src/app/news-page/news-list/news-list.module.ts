import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { NewsListComponent } from './news-list.component';

@NgModule({
  imports: [CommonModule, RouterModule, QaEnginePageStructureModule],
  declarations: [NewsListComponent],
  exports: [NewsListComponent]
})
export class NewsListModule {
}
