import { Inject, Injectable } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { BreakpointObserver } from '@angular/cdk/layout';
import { merge, Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { WINDOW } from '../public-api';

export type Breakpoint = 'smartphone' | 'tablet' | 'desktop';

@Injectable({
  providedIn: 'root'
})
export class UiService {
  constructor(
    @Inject(WINDOW) private readonly windowRef: Window,
    @Inject(DOCUMENT) private readonly documentRef: Document,
    private readonly breakpointObserver: BreakpointObserver,
  ) {
  }

  public observeOnBreakpoint(): Observable<Breakpoint> {
    return this.breakpointObserver.observe(['(max-width: 767px)', '(max-width: 1023px)']).pipe(
      map(state => {
        if (state.breakpoints['(max-width: 767px)']) {
          return 'smartphone';
        }

        if (state.breakpoints['(max-width: 1023px)']) {
          return 'tablet'
        }

        return 'desktop';
      })
    );
  }

  public setBodyOverflow(value: 'hidden' | 'auto'): void {
    if (value === 'hidden') {
      this.documentRef.body.style.paddingRight = `${this.getScrollWidth()}px`;
    } else {
      this.documentRef.body.style.paddingRight = '0';
    }

    this.documentRef.body.style.overflow = value;
  }

  private getScrollWidth(): number {
    return Math.round(this.windowRef.innerWidth - this.documentRef.body.offsetWidth);
  }
}
