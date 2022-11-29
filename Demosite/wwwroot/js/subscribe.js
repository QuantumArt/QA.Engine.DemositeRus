$(document).ready(function () {
  const form = $(".subscribe .form");
  const buttonSubmit = $(" .button[value='subscribe']", form);
  buttonSubmit.off('click').on('click', function (e) {
    e.preventDefault();
    if (!form.valid())
      return;
    var validator = $(".subscribe .form").validate();
    if ($("#newsCategory").find("input:checked").length == 0) {
      validator.showErrors({
        "checkboxes": "Please select at least one checkbox"
      });
      return;
    };
    valideAndSendData(validator);
  });
  const buttonRefreshCaptcha = $(" .button[value='captcha']", form);
  buttonRefreshCaptcha.on('click', function (e) {
    e.preventDefault();
    reloadCaptcha()
  });

  function reloadCaptcha() {
    const image = $(".img-rounded", form);
    image.attr("src", "/captcha?t=" + Math.random(5));
    $(".input[name='tokencaptcha']", form).val("");
  };

  function valideAndSendData(validator) {
    var subscriber = Object.fromEntries(new FormData(form[0]).entries());
    subscriber.newsCategory = [];
    $(".subscribe .form #newsCategory input:checked").each(function () {
      subscriber.newsCategory.push($(this).val());
    });
    subscriber.CaptchaKey = $("[data-captcha]").attr("data-captcha-defaultkey");
  
    $.ajax({
      type: "POST",
      url: "/subscribe/add",
      contentType: 'application/json',
      dataType: "json",
      data: JSON.stringify(subscriber)
    }).done(function (data) {
      console.log(data);
      if (!data.success) {
        if (data.typeError == "email") {
          validator.showErrors({
            "email": data.message
          });
        } else if (data.typeError == "captcha") {
          validator.showErrors({
            "tokencaptcha": data.message
          });
        } else if (data.typeError == "newsCategory") {
          validator.showErrors({
            "checkboxes": data.message
          });
        };
        reloadCaptcha();
      } else {
        window.location.replace(data.message);
      }
    });
  }
});


