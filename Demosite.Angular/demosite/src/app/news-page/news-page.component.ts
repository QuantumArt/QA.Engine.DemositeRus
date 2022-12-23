import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NewsPageService } from './news-page.service';
import { SiteNodeComponent, SiteNodeService } from '../services';

@Component({
  selector: 'qa-news-page',
  templateUrl: './news-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SiteNodeService, NewsPageService]
})
export class NewsPageComponent implements SiteNodeComponent {
  public get id(): number {
    return this.siteNodeService.getNodeId();
  }

  public readonly pageDetails$ = this.newsPageService.getPageDetails();

  constructor(
    private readonly siteNodeService: SiteNodeService,
    private readonly newsPageService: NewsPageService
  ) {
  }
}
