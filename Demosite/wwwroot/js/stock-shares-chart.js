"use strict";

function _toConsumableArray(arr) { return _arrayWithoutHoles(arr) || _iterableToArray(arr) || _unsupportedIterableToArray(arr) || _nonIterableSpread(); }

function _nonIterableSpread() { throw new TypeError("Invalid attempt to spread non-iterable instance.\nIn order to be iterable, non-array objects must have a [Symbol.iterator]() method."); }

function _unsupportedIterableToArray(o, minLen) { if (!o) return; if (typeof o === "string") return _arrayLikeToArray(o, minLen); var n = Object.prototype.toString.call(o).slice(8, -1); if (n === "Object" && o.constructor) n = o.constructor.name; if (n === "Map" || n === "Set") return Array.from(o); if (n === "Arguments" || /^(?:Ui|I)nt(?:8|16|32)(?:Clamped)?Array$/.test(n)) return _arrayLikeToArray(o, minLen); }

function _iterableToArray(iter) { if (typeof Symbol !== "undefined" && iter[Symbol.iterator] != null || iter["@@iterator"] != null) return Array.from(iter); }

function _arrayWithoutHoles(arr) { if (Array.isArray(arr)) return _arrayLikeToArray(arr); }

function _arrayLikeToArray(arr, len) { if (len == null || len > arr.length) len = arr.length; for (var i = 0, arr2 = new Array(len); i < len; i++) { arr2[i] = arr[i]; } return arr2; }

var totalData = [];

function loadData() {
  let period = {
    startDate: moment(0).format(stockHelper.dateFormat),
    endDate: moment().format(stockHelper.dateFormat)
  };
  var url = stockHelper.getChartDataUrl(period.startDate, period.endDate);
  $.ajax({
    dataType: "json",
    url: url,
    success: function success(data) {
      var history = _toConsumableArray(data[1].history);

      if (history.length) {
        stockHelper.startPage++;
        totalData.push.apply(totalData, _toConsumableArray(history));
        loadData();
      } else {
        stockHelper.startPage = 0;
        var stockChartData = stockHelper.formatStockData(totalData);
        $(function () {
          if (!$("#stockChart").length) return;
          var chartSettings = {
            chart: {
              renderTo: "stockChart"
            },
            plotOptions: {
              series: {
                turboThreshold: 0
              }
            },
            rangeSelector: {
              enabled: true,
              selected: 4,
              labelStyle: {
                color: "#1d2023"
              },
              inputStyle: {
                outline: 0,
                border: "1px solid rgba(188, 195, 208, 0.5)",
                padding: "4px",
                color: "#1d2023",
                boxShadow: "none",
                borderRadius: "4px",
                fontSize: "12px"
              },
              inputDateFormat: "%b %e, %Y"
            },
            series: [{
              data: stockChartData
            }]
          };
          var s = $.extend(true, stockHelper.chartDefaultSettings, chartSettings);
          Highcharts.stockChart(s);
        });
      }
    }
  });
}

loadData();
