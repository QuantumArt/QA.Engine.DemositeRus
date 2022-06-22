"use strict";

var stateClasses = {
  isOpen: "is-open",
  active: "active"
};
var selectors = {
  nav: {
    base: "[data-nav]",
    burger: "[data-nav-burger]",
    close: "[data-nav-close]",
    menuItem: "[data-menu-item]",
    menuArrow: "[data-menu-arrow]"
  },
  popup: {
    base: "[data-popup]",
    button: "[data-popup-id]",
    close: "[data-popup-close]"
  },
  tabs: {
    base: "[data-tabs]",
    navItem: "[data-tabs-nav-item]",
    item: "[data-tabs-item]"
  },
  foldbox: {
    base: "[data-foldbox]",
    head: "[data-foldbox-head]",
    body: "[data-foldbox-body]"
  },
  foldboxList: {
    base: "[data-foldbox-list]",
    on: "[data-on]",
    off: "[data-off]"
  },
  newsSlider: {
    base: "[data-news-slider]",
    nav: "[data-news-slider-nav]"
  },
  cardSlider: {
    base: "[data-cs]",
    slider: "[data-cs-slider]",
    nav: "[data-cs-nav]"
  },
  eventSlider: {
    base: "[data-es]",
    slider: "[data-es-slider]",
    counter: "[data-es-counter]",
    nav: "[data-es-nav]"
  },
  reportSlider: {
    base: "[data-rs]",
    slider: "[data-rs-slider]",
    nav: "[data-rs-nav]"
  },
  bannerSlider: {
    slider: "[data-banner-slider]"
  },
  feedbackForm: {
    form: "[data-feedback-form]",
    errorName: "[data-error-name-field]",
    errorEmailMobile: "[data-error-email-mobile-field]",
    errorText: "[data-error-text-field]"
  }
};
