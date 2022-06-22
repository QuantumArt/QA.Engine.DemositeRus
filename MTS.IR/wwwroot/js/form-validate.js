"use strict";

$(function () {
  var form = $("form");
  form.each(function () {
    $(this).attr("novalidate", "");
    $(this).validate({
      rules: {
        firstName: {
          required: true,
          lettersonly: true
        },
        lastName: {
          required: true,
          lettersonly: true
        },
        email: {
          required: true,
          emailv2: true
        },
        company: {
          required: true
        },
        activity: {
          required: true
        },
        tokencaptcha: {
          required: true
        },
        checkboxes: {
          required: false
        },
        phoneNumber: {
          required: true,
          phone: true,
        },
        commentary: {
          required: true
        },
        name: {
          required: true,
          lettersonly: true
        },
      },
      messages: {
        firstName: {
          required: "This field is required"
        },
        lastName: {
          required: "This field is required"
        },
        email: {
          required: "This field is required",
          emailv2: "This field is incorrect"
        },
        company: {
          required: "This field is required"
        },
        activity: {
          required: "This field is required"
        },
        tokencaptcha: {
          required: "This field is required"
        },
        phoneNumber: {
          required: "This field is required"
        }
      }
    });
  });
  jQuery.validator.addMethod("lettersonly", function (value, element) {
    return this.optional(element) || /^[a-z]+$/i.test(value);
  }, "Letters only please");
  jQuery.validator.addMethod("emailv2", function (value, element) {
    return this.optional(element) || /^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i.test(value);
  }, "Letters only please");
  jQuery.validator.addMethod("phone", function (value, element) {
    return this.optional(element) || /^((\+[1-9]{1,4}[ \-]*)|(\([0-9]{2,3}\)[ \-]*)|([0-9]{2,4})[ \-]*)*?[0-9]{3,4}?[ \-]*[0-9]{3,4}?$/.test(value);
  }, "Please specify a valid phone number");
});
