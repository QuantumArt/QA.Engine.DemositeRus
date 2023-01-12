import { Inject, Injectable, OnDestroy, PLATFORM_ID } from '@angular/core';
import { DOCUMENT, isPlatformBrowser } from '@angular/common';
import { Subscription } from 'rxjs';
import SwiperCore, { Navigation, Pagination, SwiperOptions } from 'swiper';
import { NavigationOptions, PaginationOptions } from 'swiper/types';
import { UiService } from '../services';

SwiperCore.use([Navigation, Pagination]);

@Injectable()
export class CardSliderService implements OnDestroy {
  private sliderRef?: SwiperCore;
  private breakpointSubscription?: Subscription;

  constructor(
    @Inject(PLATFORM_ID) private readonly platformId: Object,
    @Inject(DOCUMENT) private readonly documentRef: Document,
    private readonly uiService: UiService
  ) {
  }

  public ngOnDestroy(): void {
    this.destroySlider();
  }

  public initializeSlider(container: HTMLElement | null): void {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    this.destroySlider();

    this.breakpointSubscription = this.uiService.observeOnBreakpoint().subscribe(breakpoint => {
      this.cleanSliderControls(container);

      const sliderEl = this.initializeSliderElement(container);
      if (!sliderEl) {
        return;
      }

      const wrapperEl = this.initializeWrapperElement(container);
      if (!wrapperEl) {
        return;
      }

      this.initializeSlides(container);

      let spaceBetween = 20;

      if (breakpoint === 'laptop') {
        spaceBetween = 30;
      }

      if (breakpoint === 'desktop') {
        spaceBetween = 40;
      }

      const options: SwiperOptions = {
        spaceBetween,
        slidesPerView: breakpoint === 'smartphone' ? 1 : (breakpoint === 'tablet' ? 2 : 3),
        navigation: breakpoint === 'desktop' ? this.initializeNavigationControls(wrapperEl) : false,
        pagination: breakpoint === 'tablet' || breakpoint === 'smartphone'
          ? this.initializePaginationControls(wrapperEl)
          : false
      };

      this.sliderRef = new SwiperCore(sliderEl, options);
    });
  }

  private initializeSliderElement(container: HTMLElement | null): HTMLElement | null | undefined {
    const sliderEl = container?.querySelector<HTMLElement>('.card-slider__box');
    sliderEl?.classList.add('swiper');

    return sliderEl;
  }

  private initializeWrapperElement(container: HTMLElement | null): HTMLElement | null | undefined {
    const wrapperEl = container?.querySelector<HTMLElement>('.card-slider__slides');
    wrapperEl?.classList.add('swiper-wrapper');

    return wrapperEl;
  }

  private initializeSlides(container: HTMLElement | null): void {
    container?.querySelectorAll<HTMLElement>('.card-slider__item').forEach(item => {
      item.classList.add('swiper-slide')
    });
  }

  private initializeNavigationControls(wrapperEl: HTMLElement): NavigationOptions {
    const nextEl = this.documentRef.createElement('div');
    nextEl.classList.add('swiper-button-next');
    wrapperEl.after(nextEl);

    const prevEl = this.documentRef.createElement('div');
    prevEl.classList.add('swiper-button-prev');
    nextEl.after(prevEl);

    return { nextEl, prevEl };
  }

  private initializePaginationControls(wrapperEl: HTMLElement): PaginationOptions {
    const el = this.documentRef.createElement('div');
    el?.classList.add('card-slider__nav');
    el?.classList.add('swiper-pagination');
    wrapperEl.after(el);

    return {
      el,
      clickable: true
    };
  }

  private cleanSliderControls(container: HTMLElement | null): void {
    container?.querySelector<HTMLElement>('.swiper-button-next')?.remove();
    container?.querySelector<HTMLElement>('.swiper-button-prev')?.remove();
    container?.querySelector<HTMLElement>('.card-slider__nav')?.remove();
  }

  private destroySlider(): void {
    this.breakpointSubscription?.unsubscribe();
    this.sliderRef?.destroy();
  }
}
