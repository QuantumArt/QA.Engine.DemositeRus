import { NgModule, Type } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { APOLLO_OPTIONS, ApolloModule } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { InMemoryCache } from '@apollo/client/core';
import { QaEnginePageStructureModule, WidgetComponent } from '@quantumart/qa-engine-page-structure-angular';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { HtmlWidgetComponent, HtmlWidgetModule } from './html-widget';
import { BannerWidgetComponent, BannerWidgetModule } from './banner-widget';
import { MediaPageModule } from './media-page';
import { NewsPageModule } from './news-page';
import { RedirectPageModule } from './redirect-page';
import { SearchPageModule } from './search-page';
import { SitemapPageModule } from './sitemap-page';
import { StartPageModule } from './start-page';
import { TextPageModule } from './text-page';
import { TopMenuWidgetComponent, TopMenuWidgetModule } from './top-menu-widget';
import { NewsRoomWidgetComponent, NewsRoomWidgetModule } from './news-room-widget';
import { FoldboxListWidgetComponent, FoldboxListWidgetModule } from './foldbox-list-widget';
import { FeedbackWidgetComponent, FeedbackWidgetModule } from './feedback-widget';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'serverApp' }),
    BrowserAnimationsModule,
    ApolloModule,
    AppRoutingModule,
    BannerWidgetModule,
    FeedbackWidgetModule,
    FoldboxListWidgetModule,
    HtmlWidgetModule,
    MediaPageModule,
    NewsPageModule,
    NewsRoomWidgetModule,
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
        ['banner_widget', BannerWidgetComponent],
        ['feedback_widget', FeedbackWidgetComponent],
        ['foldboxlist_widget', FoldboxListWidgetComponent],
        ['newsroom_widget', NewsRoomWidgetComponent],
        ['top_menu_widget', TopMenuWidgetComponent]
      ]),
    }),
  ],
  providers: [
    {
      provide: APOLLO_OPTIONS,
      useFactory(httpLink: HttpLink) {
        return {
          cache: new InMemoryCache(),
          link: httpLink.create({
            uri: 'http://graphql.demositerus.dev.qsupport.ru/graphql'
          })
        }
      },
      deps: [HttpLink]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
