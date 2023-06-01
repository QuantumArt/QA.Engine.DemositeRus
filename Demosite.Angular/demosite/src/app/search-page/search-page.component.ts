import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NodeDetails } from '@quantumart/qa-engine-page-structure-angular';
import { SiteNodeComponent, SiteNodeService } from '../services';

export interface SearchPageDetails extends NodeDetails {
  title: string;
}

@Component({
  selector: 'qa-search-page',
  templateUrl: './search-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SiteNodeService]
})
export class SearchPageComponent implements SiteNodeComponent {
  public get id(): number {
    return this.siteNodeService.getNodeId();
  }

  public readonly pageDetails$ = this.siteNodeService.getDetails<SearchPageDetails>();

  constructor(private readonly siteNodeService: SiteNodeService) {
  }
}
