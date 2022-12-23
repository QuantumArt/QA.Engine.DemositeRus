import { ChangeDetectionStrategy, Component, Input, TrackByFunction } from '@angular/core';
import { MenuElement } from '@quantumart/qa-engine-page-structure-angular';

@Component({
  selector: 'qa-sitemap-level',
  templateUrl: './sitemap-level.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SitemapLevelComponent {
  @Input() public root!: MenuElement | null;
  @Input() public level!: number;

  public readonly trackById: TrackByFunction<MenuElement> = (_, item) => item.id;
}
