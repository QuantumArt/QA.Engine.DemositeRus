import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Apollo, gql } from 'apollo-angular';
import { map } from 'rxjs/operators';

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

interface NewsCategoriesQueryResult {
  newsCategories: {
    items: {
      id: number;
      alternativeTitle: string;
      alias: string;
      sortOrder: number;
    }[];
  }
}

export interface NewsCategory {
  id: number;
  alias: string;
  title: string;
  sortOrder: number;
}

@Injectable()
export class SubscribeWidgetService {
  constructor(private readonly apollo: Apollo) {
  }

  public getCategories(): Observable<NewsCategory[]> {
    return this.apollo
      .watchQuery<NewsCategoriesQueryResult>({
        query: GET_NEWS_CATEGORIES
      })
      .valueChanges.pipe(
        map(({ data }) => {
          if (!data?.newsCategories?.items?.length) {
            return [];
          }

          const categories = data.newsCategories.items
            .map(({ id, alias, alternativeTitle, sortOrder }) => ({
              id,
              alias,
              title: alternativeTitle,
              sortOrder
            }));

          categories.sort((a, b) => a.sortOrder - b.sortOrder);

          return categories;
        })
      );
  }
}
