import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterNavigationDirectiveModule } from '@quantumart/qa-engine-page-structure-angular';
import { BreadcrumbsComponent } from './breadcrumbs.component';

@NgModule({
  imports: [CommonModule, RouterNavigationDirectiveModule],
  declarations: [BreadcrumbsComponent],
  exports: [BreadcrumbsComponent],
})
export class BreadcrumbsModule {
}
