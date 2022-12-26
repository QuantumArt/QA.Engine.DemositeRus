import { LOCALE_ID, NgModule, Type } from '@angular/core';
import { BrowserModule, BrowserTransferStateModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OverlayModule } from '@angular/cdk/overlay';
import { APOLLO_OPTIONS, ApolloModule } from 'apollo-angular';
import { HttpLink } from 'apollo-angular/http';
import { InMemoryCache } from '@apollo/client/core';
import { QaEnginePageStructureModule, WidgetComponent } from '@quantumart/qa-engine-page-structure-angular';
import { TransferStateInterceptor } from './services/transfer-state.interceptor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { environment } from '../environments/environment';
import { HtmlWidgetComponent, HtmlWidgetModule } from './html-widget';
import { BannerWidgetComponent, BannerWidgetModule } from './banner-widget';
import { TopMenuWidgetComponent, TopMenuWidgetModule } from './top-menu-widget';
import { NewsRoomWidgetComponent, NewsRoomWidgetModule } from './news-room-widget';
import { FoldboxListWidgetComponent, FoldboxListWidgetModule } from './foldbox-list-widget';
import { FeedbackWidgetComponent, FeedbackWidgetModule } from './feedback-widget';
import { SubscribeWidgetComponent, SubscribeWidgetModule } from './subscribe-widget';
import '@angular/common/locales/global/ru';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'serverApp' }),
    BrowserTransferStateModule,
    BrowserAnimationsModule,
    OverlayModule,
    ApolloModule,
    AppRoutingModule,
    BannerWidgetModule,
    FeedbackWidgetModule,
    FoldboxListWidgetModule,
    HtmlWidgetModule,
    NewsRoomWidgetModule,
    TopMenuWidgetModule,
    SubscribeWidgetModule,
    QaEnginePageStructureModule.forRoot({
      widgetPlatformApiUrl: environment.WIDGET_PLATFORM_API_URL,
      layoutWidgetZones: ['SiteHeaderZone', 'SiteFooterZone'],
      widgetMapping: new Map<string, Type<WidgetComponent>>([
        ['html_widget', HtmlWidgetComponent],
        ['banner_widget', BannerWidgetComponent],
        ['feedback_widget', FeedbackWidgetComponent],
        ['foldboxlist_widget', FoldboxListWidgetComponent],
        ['newsroom_widget', NewsRoomWidgetComponent],
        ['subscribe_widget', SubscribeWidgetComponent],
        ['top_menu_widget', TopMenuWidgetComponent]
      ]),
    }),
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'ru' },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TransferStateInterceptor,
      multi: true,
    },
    {
      provide: APOLLO_OPTIONS,
      useFactory(httpLink: HttpLink) {
        return {
          cache: new InMemoryCache(),
          link: httpLink.create({
            uri: environment.GRAPHQL_DATA_API_URL
          })
        }
      },
      deps: [HttpLink]
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
