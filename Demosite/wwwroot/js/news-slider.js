"use strict";

$(function () {
  var _selectors = selectors,
    newsSlider = _selectors.newsSlider;
  var $newsSliders = $(newsSlider.base);
  $newsSliders.each(function () {
    var $sliderNav = $(this).next(newsSlider.nav);
    utils.initSlick(this, {
      infinite: false,
      dots: true,
      appendArrows: $sliderNav,
      appendDots: $sliderNav
    });
  });
});
