import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NodeDetails } from '@quantumart/qa-engine-page-structure-angular';
import { SiteNodeComponent, SiteNodeService } from '../services';

export interface StartPageDetails extends NodeDetails {
  title: string;
}

@Component({
  selector: 'qa-start-page',
  templateUrl: './start-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SiteNodeService]
})
export class StartPageComponent implements SiteNodeComponent {
  public get id(): number {
    return this.siteNodeService.getNodeId();
  }

  public readonly pageDetails$ = this.siteNodeService.getDetails<StartPageDetails>();

  constructor(private readonly siteNodeService: SiteNodeService) {
  }
}
