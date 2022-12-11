
if ($("#autoComplete").length) {
  let resultsPageInputConfig = {
    selector: "#autoComplete",
    events: {
      input: {
        focus: () => {
          if (autoCompleteJSResultsPage.input.value.length)
            autoCompleteJSResultsPage.start();
        },
        selection: (event) => {
          const selection = event.detail.selection.value;
          autoCompleteJSResultsPage.input.value = selection;
         // autoCompleteJSResultsPage.input.form.submit();
        },
        navigate: (event) => {
          autoCompleteJSResultsPage.input.value =
            event.detail.selection.value;
        },
      },
    },
    placeHolder: "Поиск",
    data: {
      src: async (query) => {
        const url = "/search/complete";
        const options = {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            Accept: "application/json",
          },
          body: JSON.stringify({ Query: query }),
        };

        const response = await fetch(url, options);

        if (response.ok) {
          const result = await response.text();
          return JSON.parse(result);
        }
      },
    },
    debounce: 300,
    threshold: 3,
    resultsList: {
      element: (list, data) => {
        if (!data.results.length) {
          const message = document.createElement("div");
          message.setAttribute("class", "no_result");
          message.innerHTML = `<span>Результатов не найдено</span>`;
          list.prepend(message);
        }
      },
      noResults: true,
    },
  };

  const autoCompleteJSResultsPage = new autoComplete(resultsPageInputConfig);
}
