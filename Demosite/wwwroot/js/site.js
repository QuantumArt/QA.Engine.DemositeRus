// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
"use strict";

(function ($) {
  function init() {
    initNewsPage();
  }

  function initNewsPage() {
    var newsPage = $("[data-news-page]");
    if (!newsPage || newsPage.length == 0)
      return;
    var year = $("#year");
    var month = $("#month");
    var debounceGetNews = debounce(getNews);

    year.on("change", loadNewsList);
    month.on("change", loadNewsList);

    initPagination();

    function initPagination() {
      if (!$(".pagination__list-item--current", newsPage).length) {
        return;
      }
      $("[aria-current='page'] > .page-link").off("click").on("click", function (e) {
        e.stopPropagation();
        e.preventDefault();
        let pageId = $(this).data("page-id");
        let yearVal = year.val() > 0 ? year.val() : null;
        let monthVal = month.val() > 0 ? month.val() : null;
        debounceGetNews(yearVal, monthVal, pageId);
      });
      $(".page-link[aria-label='Previous']").off("click").on("click", function (e) {
        e.stopPropagation();
        e.preventDefault();
        let pageId = $(".pagination__list-item--current > a").data("page-id");
        pageId = pageId - 1;
        let yearVal = year.val() > 0 ? year.val() : null;
        let monthVal = month.val() > 0 ? month.val() : null;
        debounceGetNews(yearVal, monthVal, pageId);
      });

      $(".page-link[aria-label='Next']").off("click").on("click", function (e) {
        e.stopPropagation();
        e.preventDefault();
        let pageId = $(".pagination__list-item--current > a").data("page-id");
        pageId = pageId + 1;
        let yearVal = year.val() > 0 ? year.val() : null;
        let monthVal = month.val() > 0 ? month.val() : null;
        debounceGetNews(yearVal, monthVal, pageId);
      });
    }

    function loadNewsList() {
      let yearVal = year.val() > 0 ? year.val() : null;
      let monthVal = month.val() > 0 ? month.val() : null;
      debounceGetNews(yearVal, monthVal);
    }

    function getNews(yearVal, monthVal, page = 1) {
      $("[data-news-section]", newsPage).empty();
      $.ajax({
        url: getUrl('get'),
        contentType: 'application/json',
        data: {
          year: yearVal,
          month: monthVal,
          page: page
        }
      }).done(function (html) {
        $("[data-news-section]", newsPage).html(html);
        var selectYear = $("select#year").val();
        if (dateFilterList !== undefined && dateFilterList.size) {
          var monthList = dateFilterList.has(selectYear)
            ? dateFilterList.get(selectYear)
            : new Array();
          $("select#month > option[data-month-filter]").each(function () {
            if (!monthList.length || monthList.includes(parseInt($(this).val())))
              $(this).show()
            else
              $(this).hide()
          });
        }
        initPagination();
      });
    }
  }

  function debounce(func, timeout = 500) {
    var timer = null;
    return function (...args) {
      if (timer) {
        clearTimeout(timer);
        timer = null;
      }
      timer = window.setTimeout(() => {
        func.apply(this, args);
      }, timeout);
    };
  }

  function getUrl(method) {
    let baseUrl = document.location.href.endsWith('/') ? document.location.href.slice(0, -1) : document.location.href
    return `${baseUrl}/${method}`;
  }

  $(document).ready(init);

})(jQuery);




