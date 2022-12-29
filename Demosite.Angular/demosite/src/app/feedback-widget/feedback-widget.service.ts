import { Inject, Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { FEEDBACK_API_URL, FeedbackApiUrl } from '../public-api';

export interface FeedbackRequest {
  name: string;
  mobileOrEmail: string;
  text: string;
}

export interface FeedbackRequestResult {
  succeed: boolean;
  error?: string;
}

@Injectable()
export class FeedbackWidgetService {
  constructor(
    private readonly http: HttpClient,
    @Inject(FEEDBACK_API_URL) private readonly feedbackApiUrl: FeedbackApiUrl
  ) {
  }

  public send(model: FeedbackRequest): Observable<FeedbackRequestResult> {
    return this.http.post<FeedbackRequestResult>(`${this.feedbackApiUrl}`, model).pipe(
      catchError(() => of({ succeed: false }))
    );
  }
}
