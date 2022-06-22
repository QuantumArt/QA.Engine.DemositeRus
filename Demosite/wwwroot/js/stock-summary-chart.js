"use strict";

function _toConsumableArray(arr) { return _arrayWithoutHoles(arr) || _iterableToArray(arr) || _unsupportedIterableToArray(arr) || _nonIterableSpread(); }

function _nonIterableSpread() { throw new TypeError("Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."); }

function _unsupportedIterableToArray(o, minLen) { if (!o) return; if (typeof o === "string") return _arrayLikeToArray(o, minLen); var n = Object.prototype.toString.call(o).slice(8, -1); if (n === "Object" && o.constructor) n = o.constructor.name; if (n === "Map" || n === "Set") return Array.from(o); if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)) return _arrayLikeToArray(o, minLen); }

function _iterableToArray(iter) { if (typeof Symbol !== "undefined" && iter[Symbol.iterator] != null || iter["@@iterator"] != null) return Array.from(iter); }

function _arrayWithoutHoles(arr) { if (Array.isArray(arr)) return _arrayLikeToArray(arr); }

function _arrayLikeToArray(arr, len) { if (len == null || len > arr.length) len = arr.length; for (var i = 0, arr2 = new Array(len); i < len; i++) { arr2[i] = arr[i]; } return arr2; }

var period = {
  startDate: moment().add(-3, "months").format(stockHelper.dateFormat),
  endDate: moment().format(stockHelper.dateFormat)
};
var url = stockHelper.getChartDataUrl(period.startDate, period.endDate);
$.ajax({
  dataType: "json",
  url: url,
  success: function success(data) {
    var stockChartData = stockHelper.formatStockData(_toConsumableArray(data[1].history));
    $(function () {
      var container = $("#stockChartPreview");
      if (!container.length) return;
      var chartSettings = {
        navigator: {
          enabled: false
        },
        scrollbar: {
          enabled: false
        },
        chart: {
          spacing: [32, 16, 20, 16],
          height: 200,
          borderWidth: 1,
          borderColor: "rgba(188, 195, 208, 0.5)"
        },
        xAxis: [{
          type: "datetime",
          dateTimeLabelFormats: {
            // don't display the dummy year
            month: "%e %b",
            year: "%b"
          }
        }],
        yAxis: [{
          opposite: true,
          showLastLabel: true
        }],
        series: [{
          data: stockChartData
        }]
      };
      var s = $.extend(true, stockHelper.chartDefaultSettings, chartSettings);
      Highcharts.chart("stockChartPreview", s);
    });
  }
});