import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {
  DynamicRoutesInitializer,
  InitialRequestComponent,
  LayoutWidgetsResolver,
  PageDetailsResolver
} from '@quantumart/qa-engine-page-structure-angular';
import { RedirectPageComponent, RedirectPageGuard } from './redirect-page';

const routes: Routes = [
  {
    path: '**',
    component: InitialRequestComponent,
    canActivate: [DynamicRoutesInitializer],
  },
  {
    path: 'redirect-page',
    component: RedirectPageComponent,
    canActivate: [RedirectPageGuard],
    data: {
      nodeType: 'redirect_page',
    },
  },
  {
    path: 'start-page',
    loadChildren: () => import('./start-page/start-page.module').then(m => m.StartPageModule),
    resolve: {
      details: PageDetailsResolver,
      staticWidgets: LayoutWidgetsResolver,
    },
    data: {
      nodeType: 'start_page',
    },
  },
  {
    path: 'text-page',
    loadChildren: () => import('./text-page/text-page.module').then(m => m.TextPageModule),
    resolve: {
      details: PageDetailsResolver,
      staticWidgets: LayoutWidgetsResolver,
    },
    data: {
      nodeType: 'text_page',
    },
  },
  {
    path: 'news-page',
    loadChildren: () => import('./news-page/news-page.module').then(m => m.NewsPageModule),
    resolve: {
      details: PageDetailsResolver,
      staticWidgets: LayoutWidgetsResolver,
    },
    pathMatch: 'prefix',
    data: {
      nodeType: 'news_page',
    },
  },
  {
    path: 'media-page',
    loadChildren: () => import('./media-page/media-page.module').then(m => m.MediaPageModule),
    resolve: {
      details: PageDetailsResolver,
      staticWidgets: LayoutWidgetsResolver,
    },
    data: {
      nodeType: 'media_page',
    },
  },
  {
    path: 'sitemap-page',
    loadChildren: () => import('./sitemap-page/sitemap-page.module').then(m => m.SitemapPageModule),
    resolve: {
      details: PageDetailsResolver,
      staticWidgets: LayoutWidgetsResolver,
    },
    data: {
      nodeType: 'sitemap_page',
    },
  },
  {
    path: 'search-page',
    loadChildren: () => import('./search-page/search-page.module').then(m => m.SearchPageModule),
    resolve: {
      details: PageDetailsResolver,
      staticWidgets: LayoutWidgetsResolver,
    },
    data: {
      nodeType: 'search_result_page',
    },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    initialNavigation: 'enabledBlocking',
    scrollPositionRestoration: 'enabled',
    enableTracing: false,
})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
