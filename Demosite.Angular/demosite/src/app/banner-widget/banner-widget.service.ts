import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Apollo, gql } from 'apollo-angular';

const GET_BANNERS = gql`
  query getBanners($ids: [Decimal!]) {
    bannerItems(filter: { idIn: $ids }) {
      items {
        id
        text
        sortOrder
        image
        uRL
      }
    }
  }
`;

interface BannersQueryResult {
  bannerItems: {
    items: {
      id: number;
      text?: string;
      sortOrder?: number;
      image?: string;
      uRL?: string;
    }[];
  }
}

export interface BannerItem {
  id: number;
  text?: string;
  url?: string;
  imageUrl?: string;
}

@Injectable()
export class BannerWidgetService {
  constructor(private readonly apollo: Apollo) {
  }

  public getBanners(ids: number[]): Observable<BannerItem[]> {
    return this.apollo
      .watchQuery<BannersQueryResult>({
        query: GET_BANNERS,
        variables: { ids }
      })
      .valueChanges.pipe(
        map(result => result.data?.bannerItems?.items?.map(({ id, text, uRL, image }) => ({
          id,
          text,
          url: uRL,
          imageUrl: image
        })) ?? [])
      );
  }
}
