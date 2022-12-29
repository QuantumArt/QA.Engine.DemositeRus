"use strict";

var eventSlider = {
  sliderSettings: utils.getDefaultSliderSettings(),
  setSettings: function setSettings(settings) {
    this.sliderSettings = $.extend(true, this.sliderSettings, settings);
  },
  initSlider: function initSlider() {
    var _self = this;

    var _selectors = selectors,
      eventSlider = _selectors.eventSlider;
    var $slider = $(eventSlider.slider);
    $slider.each(function () {
      _self.setSettings({
        variableWidth: true,
        appendArrows: $(this).parent().find(eventSlider.nav)
      });

      utils.initSlick($(this), _self.sliderSettings);
    });
  }
};
$(function () {
  eventSlider.initSlider();
});
