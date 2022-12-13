import { Inject, Injectable } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { BreakpointObserver } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { WINDOW } from '../public-api';

@Injectable()
export class UiService {
  constructor(
    @Inject(WINDOW) private readonly windowRef: Window,
    @Inject(DOCUMENT) private readonly documentRef: Document,
    private readonly breakpointObserver: BreakpointObserver,
  ) {
  }

  public observeOnDesktopBreakpoint(): Observable<boolean> {
    return this.breakpointObserver.observe(['(min-width: 1024px)']).pipe(map(state => state.matches));
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
