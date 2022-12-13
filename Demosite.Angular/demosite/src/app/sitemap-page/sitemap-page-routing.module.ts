import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SitemapPageComponent } from './sitemap-page.component';

const routes: Routes = [
  {
    path: '',
    component: SitemapPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class SitemapPageRoutingModule {
}
