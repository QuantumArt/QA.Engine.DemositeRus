"use strict";

var stockHelper = {
  getDatesUrl: function getDatesUrl() {
    return "https://iss.moex.com/iss/history/engines/stock/markets/shares/dates";
  },
  getDetailDataUrl: function getDetailDataUrl() {
    var _this$fields = this.fields,
        change = _this$fields.change,
        high = _this$fields.high,
        issueCapitalization = _this$fields.issueCapitalization,
        last = _this$fields.last,
        lastToPrevPrice = _this$fields.lastToPrevPrice,
        low = _this$fields.low,
        open = _this$fields.open,
        prevLegalClosePrice = _this$fields.prevLegalClosePrice,
        volToday = _this$fields.volToday;
    return "https://iss.moex.com/iss/engines/stock/markets/shares/boards/TQBR/securities/IMOEX.json?iss.meta=off&iss.json=extended&iss.only=marketdata,securities&marketdata.columns=SECID,".concat(last, ",").concat(change, ",").concat(lastToPrevPrice, ",").concat(issueCapitalization, ",").concat(volToday, ",").concat(high, ",").concat(low, ",").concat(open, "&securities.columns=PREVDATE,").concat(prevLegalClosePrice);
  },
  getChartDataUrl: function getChartDataUrl(startDate, endDate) {
    var start = this.startPage * this.pageSize;
    return "https://iss.moex.com/iss/history/engines/stock/markets/shares/sessions/total/boards/TQBR/securities/IMOEX.json?iss.meta=off&iss.json=extended&history.columns=TRADEDATE,LEGALCLOSEPRICE&sort_order=desc&from=".concat(startDate, "&till=").concat(endDate, "&start=").concat(start);
  },
  startPage: 0,
  pageSize: 100,
  dateFormat: "YYYY-MM-DD",
  fields: {
    last: "LAST",
    change: "CHANGE",
    lastToPrevPrice: "LASTTOPREVPRICE",
    issueCapitalization: "ISSUECAPITALIZATION",
    volToday: "VOLTODAY",
    high: "HIGH",
    low: "LOW",
    open: "OPEN",
    prevLegalClosePrice: "PREVLEGALCLOSEPRICE"
  },
  formatStockData: function formatStockData(data) {
    var _this = this;

    return data.map(function (point) {
      var date = moment(point.TRADEDATE, _this.dateFormat);
      var dateMs = date.valueOf();
      return {
        x: dateMs,
        y: point.LEGALCLOSEPRICE,
        name: date.format("dddd, MMMM D, YYYY")
      };
    }).sort(function (a, b) {
      return a.x - b.x;
    });
  },
  updState: function updState(el, fieldValue) {
    if (fieldValue > 0) {
      $(el).attr("data-state", "up");
    } else {
      $(el).attr("data-state", "down");
    }
  },
  chartDefaultSettings: {
    title: {
      text: ""
    },
    chart: {
      style: {
        fontFamily: "DemositeSans, Helvetica, Arial, sans-serif"
      },
      backgroundColor: 'transparent'
    },
    useDatepicker: false,
    credits: {
      enabled: false
    },
    legend: {
      enabled: false
    },
    series: [{
      name: "MOEX: IMOEX",
      data: [],
      color: "#e30611",
      tooltip: {
        valueDecimals: 2
      }
    }],
    xAxis: [{
      lineColor: "rgba(188, 195, 208, 0.5)",
      tickColor: "rgba(188, 195, 208, 0.5)",
      labels: {
        style: {
          color: "#808080",
          fontSize: "10px"
        }
      }
    }],
    yAxis: [{
      title: {
        enabled: false
      },
      gridLineColor: "rgba(188, 195, 208, 0.5)",
      labels: {
        enabled: true,
        style: {
          color: "#808080",
          fontSize: "10px"
        }
      }
    }]
  }
};
