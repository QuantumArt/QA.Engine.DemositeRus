"use strict";

var tabsCardSlider = {
  sliderSettings: utils.getDefaultSliderSettings(),
  setSettings: function setSettings(settings) {
    this.sliderSettings = $.extend(true, this.sliderSettings, settings);
  },
  getSliderSettings: function getSliderSettings() {
    return this.sliderSettings;
  },
  initSlider: function initSlider(tabNavItem) {
    var _self = this;

    var _selectors = selectors,
      tabs = _selectors.tabs,
      cardSlider = _selectors.cardSlider;
    var tabNavItemId = utils.getData(tabs.navItem, tabNavItem);
    var $sliderBoxes = $(tabNavItem).closest(tabs.base).find(cardSlider.base);
    $sliderBoxes.each(function () {
      var sliderBoxId = utils.getData(cardSlider.base, this);
      var $slider = $(this).find(cardSlider.slider);

      if (sliderBoxId === tabNavItemId) {
        var $sliderNav = $(this).find(cardSlider.nav);

        _self.setSettings({
          responsive: [{
            breakpoint: 1024,
            settings: {
              arrows: false,
              dots: true,
              appendDots: $sliderNav
            }
          }, {
            breakpoint: 768,
            settings: {
              arrows: false,
              dots: true,
              appendDots: $sliderNav
            }
            }],
        });
        utils.initSlick($slider, _self.sliderSettings);
      } else {
        utils.unSlick($slider);
      }
    });
  }
};
$(function () {
  var _selectors2 = selectors,
    tabs = _selectors2.tabs;
  $(tabs.navItem).each(function () {
    var isActive = $(this).hasClass(stateClasses.active);

    if (isActive) {
      tabsCardSlider.initSlider(this);
    }
  });
  $(tabs.navItem).on("click", function () {
    tabsCardSlider.initSlider(this);
  });
});
