import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { NodeDetails } from '@quantumart/qa-engine-page-structure-angular';

export interface NewsPageDetails extends NodeDetails {
  title: string;
  categoryid: number;
  detailstext?: string;
}

@Injectable()
export class NewsPageService {
  constructor(private readonly activatedRoute: ActivatedRoute) {
  }

  public getPageDetails(): Observable<NewsPageDetails> {
    return this.activatedRoute.parent!.data.pipe(
      filter(data => data['details']),
      map(data => data['details'] as NewsPageDetails)
    );
  }
}
