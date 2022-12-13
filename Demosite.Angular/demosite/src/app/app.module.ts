import { NgModule, Type } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { QaEnginePageStructureModule, WidgetComponent } from '@quantumart/qa-engine-page-structure-angular';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { HtmlWidgetComponent, HtmlWidgetModule } from './html-widget';
import { MediaPageModule } from './media-page';
import { NewsPageModule } from './news-page';
import { RedirectPageModule } from './redirect-page';
import { SearchPageModule } from './search-page';
import { SitemapPageModule } from './sitemap-page';
import { StartPageModule } from './start-page';
import { TextPageModule } from './text-page';
import { TopMenuWidgetComponent, TopMenuWidgetModule } from './top-menu-widget';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'serverApp' }),
    AppRoutingModule,
    HtmlWidgetModule,
    MediaPageModule,
    NewsPageModule,
    RedirectPageModule,
    SearchPageModule,
    SitemapPageModule,
    StartPageModule,
    TextPageModule,
    TopMenuWidgetModule,
    QaEnginePageStructureModule.forRoot({
      widgetPlatformApiUrl: environment.WIDGET_PLATFORM_API_URL,
      layoutWidgetZones: ['SiteHeaderZone', 'SiteFooterZone'],
      widgetMapping: new Map<string, Type<WidgetComponent>>([
        ['html_widget', HtmlWidgetComponent],
        ['top_menu_widget', TopMenuWidgetComponent]
      ]),
    }),
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
