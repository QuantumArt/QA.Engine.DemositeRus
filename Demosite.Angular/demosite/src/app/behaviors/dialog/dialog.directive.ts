import { AfterViewInit, Directive, ElementRef, Inject, OnDestroy } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { MatDialog } from '@angular/material/dialog';
import { EventHandlerCollection } from '../../utils';
import { ContentComponent } from './content.component';

@Directive({
  selector: '[qaDialogBehavior]'
})
export class DialogDirective implements AfterViewInit, OnDestroy {
  private readonly eventHandlers = new EventHandlerCollection();

  constructor(
    @Inject(DOCUMENT) private readonly documentRef: Document,
    private readonly hostEl: ElementRef<HTMLElement>,
    private readonly dialog: MatDialog
  ) {
  }

  public ngAfterViewInit(): void {
    this.hostEl.nativeElement.querySelectorAll<HTMLElement>('[data-popup-id]').forEach(element => {
      this.eventHandlers.add({
        element,
        type: 'click',
        action: () => {
          const id = element.getAttribute('data-popup-id') ?? '';
          this.dialog.open(ContentComponent, {
            data: { content: this.findContent(id) }
          })
        }
      })
    });
  }

  public ngOnDestroy(): void {
    this.eventHandlers.removeAll();
  }

  private findContent(id: string): string {
    const popup = this.documentRef.body.querySelector(`[data-popup="${id}"]`);
    return popup?.querySelector('.popup__box')?.innerHTML ?? '';
  }
}
