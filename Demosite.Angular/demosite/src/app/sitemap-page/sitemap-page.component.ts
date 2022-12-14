import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { MenuService, WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';

export interface SitemapPageDetails extends WidgetDetails {
  title: string;
}

@Component({
  selector: 'qa-sitemap-page',
  templateUrl: './sitemap-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SitemapPageComponent {
  public readonly pageDetails$: Observable<SitemapPageDetails> = this.activatedRoute.data.pipe(
    filter(data => data['details']),
    map(data => data['details'] as SitemapPageDetails),
  );

  public readonly siteStructureRoot$ = this.menuService.buildMenu(5);

  constructor(private readonly activatedRoute: ActivatedRoute, private readonly menuService: MenuService) {
  }
}
