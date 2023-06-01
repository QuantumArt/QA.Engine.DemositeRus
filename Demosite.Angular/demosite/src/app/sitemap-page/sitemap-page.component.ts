import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MenuService, NodeDetails } from '@quantumart/qa-engine-page-structure-angular';
import { SiteNodeComponent, SiteNodeService } from '../services';

export interface SitemapPageDetails extends NodeDetails {
  title: string;
}

@Component({
  selector: 'qa-sitemap-page',
  templateUrl: './sitemap-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SiteNodeService]
})
export class SitemapPageComponent implements SiteNodeComponent {
  public get id(): number {
    return this.siteNodeService.getNodeId();
  }

  public readonly pageDetails$ = this.siteNodeService.getDetails<SitemapPageDetails>();
  public readonly siteStructureRoot$ = this.menuService.buildMenu(5);

  constructor(
    private readonly siteNodeService: SiteNodeService,
    private readonly menuService: MenuService
  ) {
  }
}
