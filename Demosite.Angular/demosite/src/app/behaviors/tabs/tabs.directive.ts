﻿import { AfterViewInit, Directive, ElementRef, OnDestroy } from '@angular/core';
import { EventHandlerCollection } from '../../utils';

@Directive({
  selector: '[qaTabsBehavior]'
})
export class TabsDirective implements AfterViewInit, OnDestroy {
  private readonly eventHandlers = new EventHandlerCollection();

  constructor(private readonly hostEl: ElementRef<HTMLElement>) {
  }

  public ngAfterViewInit(): void {
    this.hostEl.nativeElement.querySelectorAll<HTMLElement>('[data-tabs]').forEach(tabsBlock => {
      tabsBlock.querySelectorAll<HTMLElement>('[data-tabs-nav-item]').forEach(element => {
        if (element.className.includes('active')) {
          this.open(tabsBlock, element);
        }

        this.eventHandlers.add({
          element,
          type: 'click',
          action: () => this.open(tabsBlock, element)
        })
      });
    });
  }

  public ngOnDestroy(): void {
    this.eventHandlers.removeAll();
  }

  private open(tabsBlock: HTMLElement, element: HTMLElement): void {
    tabsBlock.querySelectorAll<HTMLElement>('[data-tabs-nav-item]')
      .forEach(head => head.classList.remove('active'));
    element.classList.add('active');

    tabsBlock.querySelectorAll<HTMLElement>('[data-tabs-item]')
      .forEach(body => body.style.display = 'none');

    const id = element.getAttribute('data-tabs-nav-item');
    const targetBody = tabsBlock.querySelector<HTMLElement>(`[data-tabs-item="${id}"]`);

    if (targetBody) {
      targetBody.style.display = 'block';
    }
  }
}
