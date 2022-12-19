import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { WidgetComponent, WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';

export interface FeedbackWidgetDetails extends WidgetDetails {
}

const PHONE_OR_EMAIL = /^((\s*[\w.-]+@[\w-]+\.([\w-]+\.)?[A-Za-z]{2,8}\s*)|(((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}))$/;

@Component({
  selector: 'qa-feedback-widget',
  templateUrl: './feedback-widget.component.html',
  styleUrls: ['./feedback-widget.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FeedbackWidgetComponent implements WidgetComponent {
  @Input()
  public widget!: FeedbackWidgetDetails;

  public feedbackForm = new FormGroup({
    name: new FormControl('', Validators.required),
    phoneOrEmail: new FormControl('', Validators.compose([
      Validators.required,
      Validators.pattern(PHONE_OR_EMAIL)
    ])),
    text: new FormControl('', Validators.required)
  });

  constructor(private readonly http: HttpClient) {
  }

  public get name() {
    return this.feedbackForm.get('name')!;
  }

  public get phoneOrEmail() {
    return this.feedbackForm.get('phoneOrEmail')!;
  }

  public get text() {
    return this.feedbackForm.get('text')!;
  }

  public onSubmit(): void {
    //TODO: send form
    //this.http.post()
  }
}
