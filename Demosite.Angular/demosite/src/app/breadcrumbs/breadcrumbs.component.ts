import { ChangeDetectionStrategy, Component, TrackByFunction } from '@angular/core';
import { MenuElement, MenuService } from '@quantumart/qa-engine-page-structure-angular';
import { ActivatedRoute } from '@angular/router';
import { map, switchMap } from 'rxjs/operators';

const MAX_DEEP = 5;

@Component({
  selector: 'qa-breadcrumbs',
  templateUrl: './breadcrumbs.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class BreadcrumbsComponent  {
  public readonly items$ = this.menuService.buildMenu(MAX_DEEP).pipe(
    switchMap(root => this.route.data.pipe(
      map(data => ({
        root,
        nodeId: data['nodeId'] as number
      })))
    ),
    map(({ root, nodeId }) => this.findItems({ ...root, title: 'Главная' }, nodeId, MAX_DEEP).reverse())
  );

  public readonly trackById: TrackByFunction<MenuElement> = (_, item) => item.id;

  constructor(private readonly menuService: MenuService, private readonly route: ActivatedRoute) {
  }

  private findItems(node: MenuElement, nodeId: number, recursionLimit: number): MenuElement[] {
    if (node.id === nodeId) {
      return [node];
    }

    if (node.children?.length && recursionLimit > 0) {
      for (const child of node.children) {
        const items = this.findItems(child, nodeId, recursionLimit - 1);

        if (items.length) {
          items.push(node);

          return items;
        }
      }
    }

    return [];
  }
}
