import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Apollo, gql } from 'apollo-angular';

const GET_NEWS_POSTS = gql`
  query getNewsPosts($categoryId: Int!, $first: Int!) {
    newsItems(first: $first, order: [PostDateDesc], filter: { categoryEq: $categoryId }) {
      items {
        id
        title
        postDate
        brief
      }
    }
  }
`;

interface NewsPostsQueryResult {
  newsItems: {
    items: {
      id: number;
      title: string;
      postDate?: string;
      brief: string;
    }[];
  }
}

export interface NewsPost {
  id: number;
  title: string;
  url: string;
  postData?: string;
  brief?: string;
}

@Injectable()
export class NewsRoomWidgetTileService {
  constructor(private readonly apollo: Apollo) {
  }

  public getNewsPosts(categoryId: number, categoryAlias: string): Observable<NewsPost[]> {
    return this.apollo
      .watchQuery<NewsPostsQueryResult>({
        query: GET_NEWS_POSTS,
        variables: { categoryId, first: 3 }
      })
      .valueChanges.pipe(
        map(({ data }) => {
          if (!data?.newsItems?.items?.length) {
            return [];
          }

          return data.newsItems.items
            .map(({ id, title, postDate, brief }) => ({
              id,
              title,
              postData: postDate,
              brief,
              url: `/news_and_events/${categoryAlias}/details/${id}`,
            }));
        })
      );
  }
}
