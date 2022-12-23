import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Apollo, gql } from 'apollo-angular';
import { map } from 'rxjs/operators';

const GET_NEWS_POSTS = gql`
  query getNewsPosts($id: Decimal!) {
    newsItems(filter: { idEq: $id }) {
      items {
        id
        title
        postDate
        text
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
      text: string;
    }[];
  }
}

export interface NewsPost {
  id: number;
  title: string;
  url?: string;
  postDate?: string;
  text: string;
  commonText?: string;
}

@Injectable()
export class NewsDetailsService {
  constructor(private readonly apollo: Apollo) {
  }

  public getNewsPost(id: number): Observable<NewsPost | null> {
    return this.apollo
      .watchQuery<NewsPostsQueryResult>({
        query: GET_NEWS_POSTS,
        variables: { id }
      })
      .valueChanges.pipe(
        map(({ data }) => {
          if (!data?.newsItems?.items?.length) {
            return null;
          }

          return data.newsItems.items
            .map(({ id, title, postDate, text }) => ({
              id,
              title,
              postDate,
              text
            }))[0];
        })
      );
  }
}
