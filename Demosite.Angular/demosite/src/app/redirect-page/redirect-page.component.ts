import { ChangeDetectionStrategy, Component } from '@angular/core';
import { SiteNodeComponent, SiteNodeService } from '../services';

@Component({
  selector: 'qa-redirect-page',
  template: '<!-- Redirecting... -->',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SiteNodeService]
})
export class RedirectPageComponent implements SiteNodeComponent {
  public get id(): number {
    return this.siteNodeService.getNodeId();
  }

  constructor(private readonly siteNodeService: SiteNodeService) {
  }
}
