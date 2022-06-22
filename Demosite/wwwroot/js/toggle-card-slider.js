"use strict";

$(function () {
  var selectors = {
    sliderToggle: "[data-cs-toggle]",
    sliderBox: "[data-cs]",
    slider: "[data-cs-slider]",
    sliderNav: "[data-cs-nav]"
  };

  function getSliderSettings(nav) {
    return {
      slidesToShow: 3,
      slidesToScroll: 1,
      infinite: false,
      responsive: [{
        breakpoint: 1024,
        settings: {
          slidesToShow: 2,
          arrows: false,
          dots: true,
          appendDots: nav
        }
      }, {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
          arrows: false,
          dots: true,
          appendDots: nav
        }
      }]
    };
  }

  function getPropName(selector) {
    var parts = selector.slice(6, -1).split("-");
    return parts.map(function (x, i) {
      if (i !== 0) {
        var firstLetter = x[0].toUpperCase();
        x = firstLetter + x.slice(1);
      }

      return x;
    }).join("");
  }

  function initSlider(toggle) {
    var prop = getPropName(selectors.sliderToggle);
    var toggleId = $(toggle).data(prop);
    sliderBoxes.each(function () {
      var prop = getPropName(selectors.sliderBox);
      var sliderBoxId = $(this).data(prop);
      var slider = $(this).find(selectors.slider);
      var sliderNav = $(this).find(selectors.sliderNav);
      var sliderSettings = getSliderSettings(sliderNav);
      var isInitialized = slider.hasClass("slick-initialized");

      if (sliderBoxId === toggleId) {
        $(this).show();
        slider.not(".slick-initialized").slick(sliderSettings);
      } else {
        if (isInitialized) {
          slider.slick("unslick");
        }

        $(this).hide();
      }
    });
  }

  var sliderToggles = $(selectors.sliderToggle);
  var sliderBoxes = $(selectors.sliderBox);
  sliderBoxes.hide();
  sliderToggles.each(function () {
    var toggleIsActive = $(this).hasClass("active");

    if (toggleIsActive) {
      initSlider(this);
    }
  });
  sliderToggles.on("click", function () {
    $(this).siblings().removeClass("active");
    $(this).addClass("active");
    initSlider(this);
  });
});
