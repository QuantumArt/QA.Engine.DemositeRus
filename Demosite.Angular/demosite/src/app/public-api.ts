import { inject, InjectionToken } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { environment } from '../environments/environment';

export const WINDOW = new InjectionToken<(Window & typeof globalThis) | null>('Window Token', {
  providedIn: 'root',
  factory: () => inject(DOCUMENT).defaultView,
});

export type FeedbackApiUrl = typeof environment.FEEDBACK_API_URL;
export const FEEDBACK_API_URL = new InjectionToken<FeedbackApiUrl>('Feedback api url', {
  providedIn: 'root',
  factory: () => environment.FEEDBACK_API_URL,
});

export type SubscribeApiUrl = typeof environment.SUBSCRIBE_API_URL;
export const SUBSCRIBE_API_URL = new InjectionToken<SubscribeApiUrl>('Subscribe api url', {
  providedIn: 'root',
  factory: () => environment.SUBSCRIBE_API_URL,
})
