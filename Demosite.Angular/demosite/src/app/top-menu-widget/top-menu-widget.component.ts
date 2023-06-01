import { ChangeDetectionStrategy, Component, Input, TrackByFunction } from '@angular/core';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { map } from 'rxjs/operators';
import { WidgetComponent, WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';
import { UiService } from '../services';
import { TopMenuElement, TopMenuWidgetService } from './top-menu-widget.service';

export interface TopMenuWidgetDetails extends WidgetDetails {
  title: string;
}

@Component({
  selector: 'qa-top-menu-widget',
  templateUrl: './top-menu-widget.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [TopMenuWidgetService],
  animations: [
    trigger('openClose', [
      state('open', style({
        height: '*',
        opacity: 1
      })),
      state('closed', style({
        height: '0',
        opacity: 0
      })),
      transition('open => closed', [
        animate('0.35s')
      ]),
      transition('closed => open', [
        animate('0.35s')
      ]),
    ]),
  ]
})
export class TopMenuWidgetComponent implements WidgetComponent {
  @Input() public widget!: TopMenuWidgetDetails;

  public menuOpened = false;
  public readonly items$ = this.topMenuWidgetService.buildTopMenu();
  public readonly isDesktop$ = this.uiService.observeOnBreakpoint().pipe(map(state => state === 'desktop'));
  public readonly trackById: TrackByFunction<TopMenuElement> = (_, item) => item.id;

  constructor(
    private readonly uiService: UiService,
    private readonly topMenuWidgetService: TopMenuWidgetService,
  ) {
  }

  public openMenu(): void {
    if (!this.menuOpened) {
      this.menuOpened = true;
      this.uiService.setBodyOverflow('hidden');
    }
  }

  public closeMenu(): void {
    if (this.menuOpened) {
      this.menuOpened = false;
      this.uiService.setBodyOverflow('auto');
    }
  }

  public toggleSubMenu(event: Event, id: number): void {
    event.stopPropagation();
    this.topMenuWidgetService.toggleMenuElement(id);
  }
}
