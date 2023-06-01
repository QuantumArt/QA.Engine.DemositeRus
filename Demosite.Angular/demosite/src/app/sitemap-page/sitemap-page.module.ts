import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { QaEnginePageStructureModule } from '@quantumart/qa-engine-page-structure-angular';
import { SitemapPageRoutingModule } from './sitemap-page-routing.module';
import { SitemapPageComponent } from './sitemap-page.component';
import { SitemapLevelModule } from './sitemap-level';

@NgModule({
    imports: [CommonModule, SitemapPageRoutingModule, QaEnginePageStructureModule, SitemapLevelModule],
  declarations: [SitemapPageComponent],
})
export class SitemapPageModule {
}
