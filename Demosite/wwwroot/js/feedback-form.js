"use strict";

function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); Object.defineProperty(Constructor, "prototype", { writable: false }); return Constructor; }

$(function () {
  var _selectors = selectors,
    feedbackForm = _selectors.feedbackForm;
  var elFeedbackForm = document.querySelector(feedbackForm.form);
  var elErrorName = document.querySelector(feedbackForm.errorName);
  var elErrorMobileEmail = document.querySelector(feedbackForm.errorEmailMobile);
  var elErrorText = document.querySelector(feedbackForm.errorText);

  var ValidateForm = /*#__PURE__*/function () {
    function ValidateForm(el) {
      _classCallCheck(this, ValidateForm);

      this.errors = [];
      this.inputTypes = {
        text: "text",
        email: "email",
        tel: "tel",
        emailTel: "email/tel",
        textarea: "textarea"
      };
      this.emailRegex = /^\s*[\w.-]+@[\w-]+\.([\w-]+\.)?[A-Za-z]{2,8}\s*$/;
      this.telRegex = /^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$/;
      this.minNameLength = 1;
      this.minTelLength = 11;
      this.validClass = "valid";
      this.invalidClass = "invalid";
      this.telMaskOption = {
        mask: "+{7}(000)000-00-00"
      }; //   this.successModal = document.querySelector('[data-modal="success"]');
      //   this.errorModal = document.querySelector('[data-modal="error"]');

      this.form = el;
      this.inputs = this.form.querySelectorAll("input");
      this.textAreas = this.form.querySelectorAll("textarea");
      this.submitBtn = this.form.querySelector('button[type="submit"]');
      this.form.addEventListener("submit", this.onFormSubmit.bind(this));
      this.setListeners();
      this.fetchUrl = this.form.getAttribute("action");
      this.fetchMethods = {
        POST: "POST",
        GET: "GET"
      };
      this.responseCodes = {
        OK: 200,
        ERROR: 500
      };
    }

    _createClass(ValidateForm, [{
      key: "setListeners",
      value: function setListeners() {
        var _this = this;

        this.inputs.forEach(function (input) {
          input.addEventListener("blur", function () {
            _this.validateInput(input);
          });
          input.addEventListener("focus", function () {
            _this.resetInput(input);
          });
        });
        this.textAreas.forEach(function (input) {
          input.addEventListener("blur", function () {
            _this.validateInput(input);
          });
          input.addEventListener("focus", function () {
            _this.resetInput(input);
          });
        });
      }
    }, {
      key: "resetInput",
      value: function resetInput(el) {
        var container = el.parentElement;
        container.classList.remove(this.invalidClass);
        container.classList.remove(this.validClass);
      }
    }, {
      key: "setInvalid",
      value: function setInvalid(el) {
        this.resetInput(el);
        el.classList.remove(this.validClass);
        el.classList.add(this.invalidClass);
      }
    }, {
      key: "setValid",
      value: function setValid(el) {
        this.resetInput(el);
        el.classList.remove(this.invalidClass);
        el.classList.add(this.validClass);
      }
    }, {
      key: "validateEmail",
      value: function validateEmail(value, message, input) {
        var errMsg = message;

        if (!this.emailRegex.test(value)) {
          this.setInvalid(input);
          return errMsg;
        }

        this.setValid(input);
        return "";
      }
    }, {
      key: "validateTel",
      value: function validateTel(value, message, input) {
        var errMsg = message;
        var unmaskedValue = value.replace(/[\+\(\)\s\-]/g, "");

        if (unmaskedValue.length < this.minTelLength) {
          this.setInvalid(input);
          return errMsg;
        }

        this.setValid(input);
        return "";
      }
    }, {
      key: "validateName",
      value: function validateName(value, message, input) {
        var errMsg = message;

        if (value.length < this.minNameLength) {
          this.setInvalid(input);
          elErrorName.textContent = errMsg;
          return errMsg;
        }

        this.setValid(input);
        elErrorName.textContent = "";
        return "";
      }
    }, {
      key: "validateTextarea",
      value: function validateTextarea(value, message, input) {
        var errMsg = message;

        if (value.length < this.minNameLength) {
          this.setInvalid(input);
          elErrorText.textContent = errMsg;
          return errMsg;
        }

        this.setValid(input);
        elErrorText.textContent = "";
        return "";
      }
    }, {
      key: "validateEmailTelInput",
      value: function validateEmailTelInput(value, message, input) {
        var errMsg = message;
        var matchesEmail = false;
        var matchesTel = false;

        if (value.length) {
          matchesEmail = value.match(this.emailRegex);
          matchesTel = value.match(this.telRegex);
        }

        if (matchesEmail || matchesTel) {
          this.setValid(input);
          elErrorMobileEmail.textContent = "";
          return "";
        }

        this.setInvalid(input);
        elErrorMobileEmail.textContent = errMsg;
        return errMsg;
      }
    }, {
      key: "validateInput",
      value: function validateInput(input) {
        var inputType = input.getAttribute("data-type");
        var value = input.value;
        var errorMessage = input.dataset.error;

        switch (inputType) {
          case this.inputTypes.text:
            errorMessage = this.validateName(value, errorMessage, input);
            break;

          case this.inputTypes.email:
            errorMessage = this.validateEmail(value, errorMessage, input);
            break;

          case this.inputTypes.tel:
            errorMessage = this.validateTel(value, errorMessage, input);
            break;

          case this.inputTypes.emailTel:
            errorMessage = this.validateEmailTelInput(value, errorMessage, input);
            break;

          case this.inputTypes.textarea:
            errorMessage = this.validateTextarea(value, errorMessage, input);

          default:
            errorMessage = "";
        }

        return errorMessage;
      }
    }, {
      key: "getErrorMessages",
      value: function getErrorMessages() {
        var _this2 = this;

        this.errors = [];
        this.inputs.forEach(function (input) {
          var error = _this2.validateInput(input);

          if (error.length) {
            _this2.errors.push(error);
          }
        });
        this.textAreas.forEach(function (input) {
          var error = _this2.validateInput(input);

          if (error.length) {
            _this2.errors.push(error);
          }
        });
      }
    }, {
      key: "resetForm",
      value: function resetForm() {
        var _this3 = this;

        this.inputs.forEach(function (input) {
          _this3.resetInput(input);
        });
        this.form.reset();
        this.errors = [];
        this.textAreas.forEach(function (input) {
          _this3.resetInput(input);

          input.textContent = "";
        });
        localStorage.removeItem("outstaffData");
      }
    }, {
      key: "openModal",
      value: function openModal(modal) {
        if (modal) {
          modal.classList.add("modal--active");
          document.body.classList.add("scroll-lock");
        }
      }
    }, {
      key: "resolveResponse",
      value: function resolveResponse(response) {
        switch (response.status) {
          case this.responseCodes.OK:
            this.resetForm(); //   this.openModal(this.successModal);
            window.location.replace(response.url);
            break;

          case this.responseCodes.ERROR:
            //   this.openModal(this.errorModal);
            console.log("server error");
            break;

          default:
            console.log("error");
        }
      }
    }, {
      key: "fetchData",
      value: function fetchData(url, data) {
        var _this4 = this;
        try {
          fetch(url, {
            method: this.fetchMethods.POST,
            body: data
          }).then(function (response) {
            return _this4.resolveResponse(response);
          });
        } catch (error) {
          console.log(error);
        }
      }
    }, {
      key: "onFormSubmit",
      value: function onFormSubmit(evt) {
        evt.preventDefault();
        this.getErrorMessages();

        if (this.errors.length) {
          evt.preventDefault();
        } else {
          evt.preventDefault();
          this.fetchData(this.fetchUrl, new FormData(this.form));
        }
      }
    }]);

    return ValidateForm;
  }();
  if (elFeedbackForm) {
    new ValidateForm(elFeedbackForm);
  }
});
