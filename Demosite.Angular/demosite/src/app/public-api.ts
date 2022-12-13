import { inject, InjectionToken } from '@angular/core';
import { DOCUMENT } from '@angular/common';

export const WINDOW = new InjectionToken<(Window & typeof globalThis) | null>('Window Token', {
  providedIn: 'root',
  factory: () => inject(DOCUMENT).defaultView,
});
