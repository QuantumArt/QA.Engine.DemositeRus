import { ChangeDetectionStrategy, Component, Input, TrackByFunction } from '@angular/core';
import { MenuElement } from '@quantumart/qa-engine-page-structure-angular';

const HIGH_LEVEL_OCCURANCES = 2;
@Component({
  selector: 'qa-sitemap-level',
  templateUrl: './sitemap-level.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SitemapLevelComponent {
  @Input() public root!: MenuElement | null;
  @Input() public set linkLevel(level: number) {
    this._linkLevel = level + 1;
  }

  public get linkLevel(): number {
    return this._linkLevel;
  }

  public _linkLevel = 1;

  public readonly trackById: TrackByFunction<MenuElement> = (_, item) => item.id;

  public hasChildRoutes(item: MenuElement): boolean {
    return Boolean(item.children)
  }
}
