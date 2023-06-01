import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Apollo, gql } from 'apollo-angular';

const GET_NEWS_POSTS = gql`
  query getNewsPosts($categoryId: Int!) {
    newsItems(filter: { categoryEq: $categoryId }, order: [PostDateDesc]) {
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
      postDate: string;
      brief: string;
    }[];
  }
}

export interface NewsPost {
  id: number;
  title: string;
  url?: string;
  postDate: string;
  brief?: string;
}

@Injectable()
export class NewsListService {
  constructor(private readonly apollo: Apollo) {
  }

  public getNewsPosts(categoryId: number): Observable<NewsPost[]> {
    return this.apollo
      .watchQuery<NewsPostsQueryResult>({
        query: GET_NEWS_POSTS,
        variables: { categoryId }
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
              postDate,
              brief
            }));
        })
      );
  }
}
