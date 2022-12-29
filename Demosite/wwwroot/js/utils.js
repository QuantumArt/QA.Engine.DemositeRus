"use strict";

const COUNT_SLIDES_SHOW = 3;

var utils = {
  _getPropName: function _getPropName(dataAttr) {
    if (!dataAttr) return "";
    var parts = dataAttr.slice(6, -1).split("-");
    return parts.map(function (x, i) {
      if (i !== 0) {
        var firstLetter = x[0].toUpperCase();
        x = firstLetter + x.slice(1);
      }

      return x;
    }).join("");
  },
  _getScrollWidth: function _getScrollWidth() {
    var windowWidth = window.innerWidth;
    var bodyWidth = $("body").outerWidth();
    return Math.round(windowWidth - bodyWidth);
  },
  getData: function getData(dataAttr, el) {
    var prop = this._getPropName(dataAttr);

    return $(el).data(prop);
  },
  getSelectorWithKey: function getSelectorWithKey(selector, key) {
    return "".concat(selector.slice(0, -1), "=").concat(key, "]");
  },
  setBodyOverflow: function setBodyOverflow(value) {
    var scrollWidth = this._getScrollWidth();

    if (value === "hidden") {
      $("body").css("padding-right", scrollWidth);
    } else {
      $("body").css("padding-right", 0);
    }

    $("body").css("overflow", value);
  },
  // sliders utils
  getDefaultSliderSettings: function getDefaultSliderSettings() {
    return {
      slidesToShow: COUNT_SLIDES_SHOW,
      slidesToScroll: 1,
      infinite: false,
      responsive: [{
        breakpoint: 1024,
        settings: {
          slidesToShow: 2
        }
      }, {
        breakpoint: 768,
        settings: {
          slidesToShow: 1
        }
      }]
    };
  },
  initSlick: function initSlick(slider, settings) {
    $(slider).not(".slick-initialized").slick(settings);
  },
  unSlick: function unSlick(slider) {
    var isInitialized = slider.hasClass("slick-initialized");

    if (isInitialized) {
      slider.slick("unslick");
    }
  },
  // end sliders utils
  formatSum: function formatSum(sum) {
    var showDecimal = arguments.length > 1 && arguments[1] !== undefined ? arguments[1] : true;

    if (!sum) {
      return "";
    }

    var result = sum;

    if (Math.round(result) !== result) {
      result = result.toFixed(2);
    } else {
      result = showDecimal ? result + ".00" : result;
    }

    result = result.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ");
    return result;
  }
};
