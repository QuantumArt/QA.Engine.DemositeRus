"use strict";

$(function () {
  if (!$("#shareholderChart").length) return;
  Highcharts.chart("shareholderChart", {
    chart: {
      backgroundColor: "transparent",
      plotBackgroundColor: null,
      plotBorderWidth: null,
      plotShadow: false,
      type: "pie",
      center: ["50%", "50%"],
      style: {
        fontFamily: "MTSSans"
      }
    },
    title: null,
    credits: {
      enabled: false
    },
    tooltip: {
      backgroundColor: "rgba(255,255,255,1)",
      pointFormat: "<b>{point.y}%</b>"
    },
    legend: {
      enabled: true
    },
    plotOptions: {
      pie: {
        startAngle: -30,
        slicedOffset: 15,
        showInLegend: true,
        borderWidth: 0,
        dataLabels: {
          enabled: false
        }
      }
    },
    series: [{
      name: "Shareholder",
      innerSize: "70%",
      data: [{
        name: "Sistema PJSFC",
        y: 42.085,
        color: "#E30611"
      }, {
        name: "MTS PJSC's subsidiaries",
        color: "#58595B",
        y: 16.33
      }, {
        name: "Public free float",
        y: 41.59,
        color: "#ADAFAF"
      }]
    }]
  });
});
