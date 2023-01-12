import { AfterViewInit, Directive, ElementRef, Input, OnDestroy } from '@angular/core';
import { slideDown, slideStop, slideUp } from 'slide-anim';
import { EventHandlerCollection } from '../../utils';

const ACCORDION_DURATION = 1000;

@Directive({
  selector: '[qaFoldboxBehavior]'
})
export class FoldboxDirective implements AfterViewInit, OnDestroy {
  @Input() public foldboxToggleAllSelector?: string;

  private opened = false;
  private readonly eventHandlers = new EventHandlerCollection();

  constructor(private readonly hostEl: ElementRef<HTMLElement>) {
  }

  public ngAfterViewInit(): void {
    if (this.foldboxToggleAllSelector) {
      this.hostEl.nativeElement.querySelectorAll<HTMLElement>(this.foldboxToggleAllSelector).forEach(element => {
        this.eventHandlers.add({
          element,
          type: 'click',
          action: () => {
            this.hostEl.nativeElement.querySelectorAll<HTMLElement>('[data-foldbox]').forEach(element => {
              this.toggle(element, this.opened);
            });
            this.opened = !this.opened;
          }
        });
      });
    } else {
      this.hostEl.nativeElement.querySelectorAll<HTMLElement>('[data-foldbox]').forEach(element => {
        const head = element.querySelector<HTMLElement>('[data-foldbox-head]');
        if (head) {
          this.eventHandlers.add({
            element: head,
            type: 'click',
            action: () => {
              this.toggle(element, element.className.includes('active'));
            }
          });
        }
      });
    }
  }

  public ngOnDestroy(): void {
    this.eventHandlers.removeAll();
  }

  private toggle(element: HTMLElement, opened: boolean): void {
    if (!opened) {
      element.classList.add('active');
    } else {
      element.classList.remove('active');
    }

    const body = element.querySelector<HTMLElement>('[data-foldbox-body]');
    if (body) {
      if (!opened) {
        slideStop(body);
        slideDown(body, {duration: ACCORDION_DURATION});
      } else {
        slideStop(body);
        slideUp(body, {duration: ACCORDION_DURATION});
      }
    }
  }
}
