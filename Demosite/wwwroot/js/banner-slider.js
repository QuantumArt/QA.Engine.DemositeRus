"use strict";

$(function () {
  var _selectors = selectors,
    bannerSlider = _selectors.bannerSlider;
  var $bannerSliders = $(bannerSlider.slider);
  var autoplaySpeed = $bannerSliders.data("autoplay-speed");
  $bannerSliders.each(function () {
    utils.initSlick(this, {
      infinite: true,
      dots: true,
      autoplay: true,
      autoplaySpeed: autoplaySpeed !== null && autoplaySpeed !== void 0 ? (parseInt(autoplaySpeed) * 1000) : 5000
    });
  });
});
