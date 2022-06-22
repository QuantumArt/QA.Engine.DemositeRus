"use strict";

function ownKeys(object, enumerableOnly) { var keys = Object.keys(object); if (Object.getOwnPropertySymbols) { var symbols = Object.getOwnPropertySymbols(object); enumerableOnly && (symbols = symbols.filter(function (sym) { return Object.getOwnPropertyDescriptor(object, sym).enumerable; })), keys.push.apply(keys, symbols); } return keys; }

function _objectSpread(target) { for (var i = 1; i < arguments.length; i++) { var source = null != arguments[i] ? arguments[i] : {}; i % 2 ? ownKeys(Object(source), !0).forEach(function (key) { _defineProperty(target, key, source[key]); }) : Object.getOwnPropertyDescriptors ? Object.defineProperties(target, Object.getOwnPropertyDescriptors(source)) : ownKeys(Object(source)).forEach(function (key) { Object.defineProperty(target, key, Object.getOwnPropertyDescriptor(source, key)); }); } return target; }

function _defineProperty(obj, key, value) { if (key in obj) { Object.defineProperty(obj, key, { value: value, enumerable: true, configurable: true, writable: true }); } else { obj[key] = value; } return obj; }

$.ajax({
  dataType: "json",
  url: stockHelper.getDetailDataUrl(),
  success: function success(data) {
    var stockData = _objectSpread(_objectSpread({}, data[1].marketdata[0]), data[1].securities[0]);

    $(function () {
      var isUpdPriceState = false;
      $("[data-stock-date]").text("As of ".concat(moment().format("LLL"), " MSK"));
      $("[data-key]").each(function () {
        var keyValue = $(this).data("key");
        var value = stockData[keyValue];

        switch (keyValue) {
          case stockHelper.fields.last:
            if (!isUpdPriceState) {
              stockHelper.updState(this, stockData[stockHelper.fields.change]);
            }

            isUpdPriceState = true;
            value = utils.formatSum(value);
            break;

          case stockHelper.fields.change:
          case stockHelper.fields.lastToPrevPrice:
            stockHelper.updState(this, stockData[stockHelper.fields.change]);
            break;

          case stockHelper.fields.change:
            value = utils.formatSum(value);
            break;

          case stockHelper.fields.volToday:
            value = utils.formatSum(value, false);
            break;

          case stockHelper.fields.issueCapitalization:
            value = utils.formatSum(value / 1e9);
            break;

          default:
            value = utils.formatSum(value);
        }

        $(this).text(value);
      });
    });
  }
});