$(document).ready(function () {
  const form = $(".feedback .form");
  const buttonSubmit = $(" .button[value='feedback']", form);
  buttonSubmit.off('click').on('click', function (e) {
    e.preventDefault();
    if (!form.valid())
      return;
    var validator = $(".feedback .form").validate();
    valideAndSendData(validator);
  });

  function valideAndSendData(validator) {
    var feedback = Object.fromEntries(new FormData(form[0]).entries());

    $.ajax({
      type: "POST",
      url: "/feedback/sendfeedback",
      contentType: 'application/json',
      dataType: "json",
      data: JSON.stringify(feedback)
    }).done(function (data) {
      console.log(data);
      if (data.success) {
        window.location.replace(data.message);
      } 
    });
  }
});


