import { isPlatformServer } from '@angular/common';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { makeStateKey, StateKey, TransferState } from '@angular/platform-browser';
import { Observable, of } from 'rxjs';
import { tap } from 'rxjs/operators';

const IGNORE_QUERY_PARAMS_URLS = ['/Site/structure'];

@Injectable()
export class TransferStateInterceptor implements HttpInterceptor {
    constructor(
        private readonly transferState: TransferState,
        @Inject(PLATFORM_ID) private readonly platformId: Object,
    ) {}

    public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (request.method !== 'GET') {
            return next.handle(request);
        }

        const key = this.makeStateKey(request);

        if (isPlatformServer(this.platformId)) {
            return next.handle(request).pipe(
                tap(event => {
                    this.transferState.set(key, (<HttpResponse<any>>event).body);
                }),
            );
        }
        const storedResponse = this.transferState.get<any>(key, null);
        if (storedResponse) {
            const response = new HttpResponse({ body: storedResponse, status: 200 });
            this.transferState.remove(key);

            return of(response);
        }

        return next.handle(request);
    }

    private makeStateKey(request: HttpRequest<any>): StateKey<string> {
        const { pathname, search } = new URL(request.urlWithParams);

        return makeStateKey<string>(
          `${pathname}${IGNORE_QUERY_PARAMS_URLS.some(url => pathname.startsWith(url)) ? '' : search}`
        );
    }
}
