import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Apollo, gql } from 'apollo-angular';

const GET_FOLDBOX_ITEMS = gql`
  query getFoldboxItems($ids: [Decimal!]) {
    foldBoxListItems(filter: { idIn: $ids }) {
      items {
        id
        title
        text
        sortOrder
      }
    }
  }
`;

interface FoldboxListItemsQueryResult {
  foldBoxListItems: {
    items: {
      id: number;
      title: string;
      text: string;
      sortOrder: number;
    }[];
  }
}

export interface FoldboxListItem {
  id: number;
  title: string;
  text: string;
  sortOrder: number;
}

@Injectable()
export class FoldboxListWidgetService {
  constructor(private readonly apollo: Apollo) {
  }

  public getBanners(ids: number[]): Observable<FoldboxListItem[]> {
    return this.apollo
      .watchQuery<FoldboxListItemsQueryResult>({
        query: GET_FOLDBOX_ITEMS,
        variables: { ids }
      })
      .valueChanges.pipe(
        map(({ data }) => {
          if (!data?.foldBoxListItems?.items?.length) {
            return [];
          }

          const items = data.foldBoxListItems.items
            .map(({ id, title, text, sortOrder }) => ({
              id,
              title,
              text,
              sortOrder
            }));

          items.sort((a, b) => a.sortOrder - b.sortOrder);

          return items;
        })
      );
  }
}
