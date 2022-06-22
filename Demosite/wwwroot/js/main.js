"use strict";

moment.locale();
$(function () {
  // Document is ready
  // navigation
  var _selectors = selectors,
    nav = _selectors.nav;
  var $nav = $(nav.base);
  $(nav.menuArrow).on("click", function (e) {
    e.stopPropagation();
    var $menuItem = $(this).closest(nav.menuItem);
    var menuItemId = $menuItem.data("id");
    var $menu = $menuItem.find("[data-parentId=".concat(menuItemId, "]"));
    var isOpen = $menuItem.hasClass(stateClasses.isOpen);

    if (isOpen) {
      $menuItem.removeClass(stateClasses.isOpen);
      $menu.slideUp();
    } else {
      $menuItem.addClass(stateClasses.isOpen);
      $menu.slideDown();
    }
  });
  $(nav.burger).on("click", function () {
    $nav.addClass("is-open-mobile");
    utils.setBodyOverflow("hidden");
  });
  $(nav.close).on("click", function () {
    $nav.removeClass("is-open-mobile");
    utils.setBodyOverflow("auto");
  }); //popups

  var _selectors2 = selectors,
    popup = _selectors2.popup;
  var $popups = $(popup.base);
  $popups.hide();
  $(popup.close).on("click", function () {
    $(this).closest(popup.base).fadeOut(200);
    setTimeout(function () {
      return utils.setBodyOverflow("auto");
    }, 200);
  });
  $(popup.button).on("click", function () {
    var id = utils.getData(popup.button, this);
    $popups.each(function () {
      var popupId = utils.getData(popup.base, this);

      if (popupId === id) {
        $(this).fadeIn(200);
        utils.setBodyOverflow("hidden");
      } else {
        $(this).hide();
      }
    });
  }); // tabs

  var _selectors3 = selectors,
    tabs = _selectors3.tabs;

  function initTabs(tabsNavItem) {
    var $tabs = $(tabsNavItem).closest(tabs.base);
    var $tabsItems = $tabs.find(tabs.item);
    var $tabsNavItems = $tabs.find(tabs.navItem);
    var id = utils.getData(tabs.navItem, tabsNavItem);
    var tabsItem = utils.getSelectorWithKey(tabs.item, id);
    $tabsItems.hide();
    $tabsNavItems.removeClass(stateClasses.active);
    $(tabsNavItem).addClass(stateClasses.active);
    $tabs.find(tabsItem).show();
  }

  var $tabsNavItems = $(tabs.navItem);
  $tabsNavItems.each(function () {
    var isActive = $(this).hasClass(stateClasses.active);

    if (isActive) {
      initTabs(this);
    }
  });
  $tabsNavItems.on("click", function () {
    initTabs(this);
  }); // foldbox

  var _selectors4 = selectors,
    foldbox = _selectors4.foldbox,
    foldboxList = _selectors4.foldboxList;

  function openFoldbox(el) {
    $(el).addClass(stateClasses.active);
    $(el).find(foldbox.body).slideDown();
  }

  function closeFoldbox(el) {
    $(el).removeClass(stateClasses.active);
    $(el).find(foldbox.body).slideUp();
  }

  function toggleBtnText(el) {
    var $btn = el.find(foldboxList.on);
    var onValue = utils.getData(foldboxList.on, $btn);
    var offValue = utils.getData(foldboxList.off, $btn);
    var $foldboxes = el.find(foldbox.base);
    var allFoldboxStates = [];
    $foldboxes.each(function () {
      var state = $(this).hasClass(stateClasses.active);
      allFoldboxStates.push(state);
    });
    var isEveryOpened = allFoldboxStates.every(function (x) {
      return !!x;
    });
    var isEveryClosed = allFoldboxStates.every(function (x) {
      return !x;
    });

    if (isEveryOpened) {
      $btn.text(offValue);
      return;
    }

    if (isEveryClosed) {
      $btn.text(onValue);
      return;
    }
  }

  $(foldbox.head).on("click", function () {
    var currentFoldbox = $(this).parent();
    var isActive = currentFoldbox.hasClass(stateClasses.active);
    var fbList = $(this).closest(foldboxList.base);

    if (isActive) {
      closeFoldbox(currentFoldbox);
    } else {
      openFoldbox(currentFoldbox);
    }

    if (fbList.length) {
      toggleBtnText(fbList);
    }
  });
  $(foldboxList.on).on("click", function () {
    var onValue = utils.getData(foldboxList.on, this);
    var currentValue = $(this).text();
    var currentFoldboxList = $(this).closest(foldboxList.base);
    var foldboxes = currentFoldboxList.find(foldbox.base);
    foldboxes.each(function () {
      if (onValue !== currentValue) {
        closeFoldbox(this);
      } else {
        openFoldbox(this);
      }
    });
    toggleBtnText(currentFoldboxList);
  });
});
