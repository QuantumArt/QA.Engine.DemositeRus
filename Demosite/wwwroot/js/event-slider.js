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
    $slider.on("init reInit afterChange", function (event, slick, currentSlide, nextSlide) {
      //currentSlide is undefined on init -- set it to 0 in this case (currentSlide is 0 based)
      var i = (currentSlide ? currentSlide : 0) + 1;
      $(this).parent().find(eventSlider.counter).text(i + "/" + slick.slideCount);
    });
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
