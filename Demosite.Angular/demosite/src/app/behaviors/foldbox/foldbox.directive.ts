import { AfterViewInit, Directive, ElementRef, OnDestroy } from '@angular/core';
import { EventHandlerCollection } from '../../utils';

@Directive({
  selector: '[qaFoldboxBehavior]'
})
export class FoldboxDirective implements AfterViewInit, OnDestroy {
  private readonly eventHandlers = new EventHandlerCollection();

  constructor(private readonly hostEl: ElementRef<HTMLElement>) {
  }

  public ngAfterViewInit(): void {
    this.hostEl.nativeElement.querySelectorAll<HTMLElement>('[data-foldbox]').forEach(element => {
      this.eventHandlers.add({
        element,
        type: 'click',
        action: () => {
          element.classList.toggle('active');
          const foldboxBody = element.querySelector<HTMLElement>('[data-foldbox-body]');
          if (foldboxBody) {
            foldboxBody.style.display = foldboxBody.style.display === 'block' ? 'none' : 'block';
          }
        }
      });
    });
  }

  public ngOnDestroy(): void {
    this.eventHandlers.removeAll();
  }
}
