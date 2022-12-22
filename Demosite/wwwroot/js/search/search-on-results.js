$(".autoComplete").each(function () {
  let resultsPageInputConfig = {
    selector: () => {
      return this; // Any valid selector
    },
    events: {
      input: {
        focus: () => {
          if (autoCompleteJSResultsPage.input.value.length)
            autoCompleteJSResultsPage.start();
        },
        selection: (event) => {
          const selection = event.detail.selection.value;
          autoCompleteJSResultsPage.input.value = selection;
        },
        navigate: (event) => {
          autoCompleteJSResultsPage.input.value = event.detail.selection.value;
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
          const result = await response.json();
          return result;
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
});

class SearchInputValidator {
  constructor(form) {
    this.form = form;
    this.errorMessage = "";
    this.activeErrorMessage = false;
    this.invalid = true;

    this.searchBtnFake = this.form.querySelector(".search-btn__fake");
    this.submitBtn = this.form.querySelector('button[type="submit"]');
    this.input = this.form.querySelector("input");
    this.errorBlock = this.form.querySelector(".search-form__error");

    this.minInputLength = this.input.minLength
      ? Number(this.input.minLength)
      : 0;

    this.setListeners();
  }

  setListeners() {
    if (this.input) {
      this.input.addEventListener("keyup", () => {
        this.validate();
      });

      this.searchBtnFake.addEventListener(
        "click",
        () => {
          if (this.invalid) {
            this.showError();
          }
        },
        true
      );

      this.input.addEventListener("focus", () => {
        if (this.activeErrorMessage) {
          this.hideError();
        }
      });
    }
  }

  validate() {
    if (this.submitBtn) {
      if (this.input.value.length < this.minInputLength) {
        this.setDisabledSubmit();
        this.errorMessage = `Минимальная длина ${
          this.minInputLength
        } ${this.generateEnding(this.minInputLength)}`;

        this.invalid = true;
      } else {
        this.setEnabledSubmit();
        this.invalid = false;
      }
    }
  }
  setDisabledSubmit() {
    this.submitBtn.disabled = true;
  }
  setEnabledSubmit() {
    this.submitBtn.disabled = false;
  }

  showError() {
    this.activeErrorMessage = true;
    if (this.errorBlock) {
      this.errorBlock.textContent = this.errorMessage;
      this.errorBlock.classList.remove("search-form__error--hidden");
      this.errorBlock.classList.add("search-form__error--visible");
    }
  }

  hideError() {
    this.activeErrorMessage = false;
    if (this.errorBlock) {
      this.errorBlock.textContent = this.errorMessage;
      this.errorBlock.classList.remove("search-form__error--visible");
      this.errorBlock.classList.add("search-form__error--hidden");
    }
  }

  generateEnding(value) {
    const words = ["символ", "символа", "символов"];
    const num = value % 10;
    if (value > 10 && value < 20) {
      return words[2];
    }
    if (num > 1 && num < 5) {
      return words[1];
    }
    if (num == 1) {
      return words[0];
    }
    return words[2];
  }
}

$(".search-form ").each(function () {
  const searchForm = new SearchInputValidator(this);
  searchForm.validate();
});
