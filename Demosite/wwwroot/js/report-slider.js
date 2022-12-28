"use strict";

var reportSlider = {
  sliderSettings: utils.getDefaultSliderSettings(),
  setSettings: function setSettings(settings) {
    this.sliderSettings = $.extend(true, this.sliderSettings, settings);
  },
  initSlider: function initSlider() {
    var _self = this;

    var _selectors = selectors,
        reportSlider = _selectors.reportSlider;
    var $slider = $(reportSlider.slider);
    $slider.each(function () {
      _self.setSettings({
        appendArrows: $(this).parent().find(reportSlider.nav)
      });
      _self.setSettings({
        slidesToShow: 3,
      });
      utils.initSlick($(this), _self.sliderSettings);
    });
  }
};
$(function () {
  reportSlider.initSlider();
});
