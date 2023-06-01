import { Injectable } from '@angular/core';
import { MenuElement, MenuService } from '@quantumart/qa-engine-page-structure-angular';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';

export interface TopMenuElement {
  id: number;
  title?: string;
  href?: string;
  children: TopMenuElement[];
  isOpened: boolean;
}

@Injectable()
export class TopMenuWidgetService {
  private readonly openedElements$ = new BehaviorSubject<Set<number>>(new Set<number>());

  constructor(private readonly menuService: MenuService) {
  }

  public buildTopMenu(): Observable<TopMenuElement[]> {
    return this.menuService.buildMenu(3).pipe(
      switchMap(menu => this.openedElements$.pipe(map(openedElements => ({ menu, openedElements })))),
      map(({ menu, openedElements }) => this.mapToTopMenu(menu, openedElements, 3).children)
    );
  }

  public toggleMenuElement(id: number): void {
    const openedElements = new Set<number>(this.openedElements$.value);
    if (openedElements.has(id)) {
      openedElements.delete(id);
    } else {
      openedElements.add(id);
    }

    this.openedElements$.next(openedElements);
  }

  private mapToTopMenu(menuElement: MenuElement, openedElements: Set<number>, deep: number): TopMenuElement {
    const topMenuElement: TopMenuElement = {
      ...menuElement,
      children: [],
      isOpened: openedElements.has(menuElement.id)
    };

    if (menuElement.children?.length && deep > 0) {
      topMenuElement.children = menuElement.children.map(child => this.mapToTopMenu(child, openedElements, deep - 1));
    }

    return topMenuElement;
  }
}
