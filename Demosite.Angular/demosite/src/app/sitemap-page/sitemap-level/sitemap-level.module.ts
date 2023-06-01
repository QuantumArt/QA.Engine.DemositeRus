import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SitemapLevelComponent } from './sitemap-level.component';
import { RouterNavigationDirectiveModule } from '@quantumart/qa-engine-page-structure-angular';

@NgModule({
  imports: [CommonModule, RouterNavigationDirectiveModule],
  declarations: [SitemapLevelComponent],
  exports: [
    SitemapLevelComponent
  ]
})
export class SitemapLevelModule {
}
