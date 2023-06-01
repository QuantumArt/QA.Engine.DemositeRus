import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Apollo, gql } from 'apollo-angular';

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

interface NewsBlocksQueryResult {
  newsCategories: {
    items: {
      id: number;
      alternativeTitle: string;
      alias: string;
      sortOrder: number;
    }[];
  }
}

export interface NewsBlock {
  id: number;
  alias: string;
  title: string;
  url: string;
}

@Injectable()
export class NewsRoomWidgetService {
  constructor(private readonly apollo: Apollo) {
  }

  public getNewsBlocks(): Observable<NewsBlock[]> {
    return this.apollo
      .watchQuery<NewsBlocksQueryResult>({
        query: GET_NEWS_CATEGORIES
      })
      .valueChanges.pipe(
        map(({ data }) => {
          if (!data?.newsCategories?.items?.length) {
            return [];
          }

          const blocks = data.newsCategories.items
            .map(({ id, alias, alternativeTitle, sortOrder }) => ({
              id,
              alias,
              title: alternativeTitle,
              url: `/news_and_events/${alias}`,
              sortOrder
            }));

          blocks.sort((a, b) => a.sortOrder - b.sortOrder);

          return blocks;
        })
      );
  }
}
