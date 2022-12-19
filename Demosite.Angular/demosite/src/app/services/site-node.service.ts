import { Injectable } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { NodeDetails } from '@quantumart/qa-engine-page-structure-angular';

export interface SiteNodeComponent {
  get id(): number;
}

@Injectable()
export class SiteNodeService {
  constructor(private readonly activatedRoute: ActivatedRoute) {
  }

  public getNodeId(): number {
    return this.activatedRoute.snapshot.data['nodeId'] as number;
  }

  public getDetails<T extends NodeDetails>(): Observable<T> {
    return this.activatedRoute.data.pipe(
      filter(data => data['details']),
      map(data => data['details'] as T),
    );
  }
}
