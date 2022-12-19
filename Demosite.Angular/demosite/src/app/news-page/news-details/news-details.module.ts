import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { BreadcrumbsModule } from '../../breadcrumbs';
import { NewsDetailsComponent } from './news-details.component';
import { SafePipeModule } from '../../pipes';

@NgModule({
  imports: [CommonModule, RouterModule, BreadcrumbsModule, SafePipeModule],
  declarations: [NewsDetailsComponent],
  exports: [NewsDetailsComponent]
})
export class NewsDetailsModule {
}
