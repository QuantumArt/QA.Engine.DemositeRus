import { Inject, Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { Apollo, gql } from 'apollo-angular';
import { map, takeUntil } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { SubscribeApiUrl, SUBSCRIBE_API_URL } from '../public-api';
import { SubscribeRequest, NewsCategory, NewsCategoriesQueryResult, SubscribeRequestResult } from './subscribe-widget.types';

const GET_NEWS_CATEGORIES = gql`
  query getNewsCategories {
    newsCategories(filter: { showOnStartEq: true }) {
      items {
        id
        alternativeTitle
        alias
        sortOrder
      }
    }
  }
`;

@Injectable()
export class SubscribeWidgetService implements OnDestroy {
  public readonly isSuccess$ = new BehaviorSubject<boolean>(false);
  public readonly isFailure$ = new BehaviorSubject<boolean>(false);
  private readonly ngUnsubscribe$ = new Subject<void>();

  constructor(
    private readonly http: HttpClient,
    private readonly apollo: Apollo,
    @Inject(SUBSCRIBE_API_URL) private readonly subscribeApiUrl: SubscribeApiUrl
  ) {}

  ngOnDestroy(): void {
    this.ngUnsubscribe$.next();
    this.ngUnsubscribe$.complete();
  }

  public sendForm(model: SubscribeRequest): void {
    this.createConnectRequest(model)
      .pipe(takeUntil(this.ngUnsubscribe$))
      .subscribe({
        next: (requestData) => {
          if (!requestData || !requestData.succeed) {
            this.isFailure$.next(true);

            return;
          }
          this.isSuccess$.next(true);
        },
        error: () => {
          this.isFailure$.next(true);
        },
      });
  }

  public getCategories(): Observable<NewsCategory[]> {
    return this.apollo
      .watchQuery<NewsCategoriesQueryResult>({
        query: GET_NEWS_CATEGORIES,
      })
      .valueChanges.pipe(
        map(({ data }) => {
          if (!data?.newsCategories?.items?.length) {
            return [];
          }

          const categories = data.newsCategories.items.map(
            ({ id, alias, alternativeTitle, sortOrder }) => ({
              id,
              alias,
              title: alternativeTitle,
              sortOrder,
            })
          );

          categories.sort((a, b) => a.sortOrder - b.sortOrder);

          return categories;
        })
      );
  }

  private createConnectRequest(
    data: SubscribeRequest
  ): Observable<SubscribeRequestResult> {
    return this.http.post<SubscribeRequestResult>(
      `${this.subscribeApiUrl}`,
      data
    );
  }
}
