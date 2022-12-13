import { Inject, Injectable } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { WINDOW } from '../public-api';

@Injectable()
export class UiService {
  constructor(
    @Inject(WINDOW) private readonly windowRef: Window,
    @Inject(DOCUMENT) private readonly documentRef: Document
  ) {
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
