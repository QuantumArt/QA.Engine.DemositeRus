// @ts-nocheck
//import defaultConfig from "./search-default-config/default-config";
//import autoComplete from "@tarekraafat/autocomplete.js";
if ($("#autoComplete").length) {
  const resultsPageInputConfig = {
    ...defaultConfig,
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
          //autoCompleteJSResultsPage.input.form.submit();
        },
        navigate: (event) => {
          autoCompleteJSResultsPage.input.value =
            event.detail.selection.value;
        },
      },
    },
  };

  const autoCompleteJSResultsPage = new autoComplete(resultsPageInputConfig);
}
