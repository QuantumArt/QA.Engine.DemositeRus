import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { WidgetComponent, WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';
import { FeedbackRequestResult, FeedbackWidgetService } from './feedback-widget.service';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

export interface FeedbackWidgetDetails extends WidgetDetails {
}

const PHONE_OR_EMAIL = /^((\s*[\w.-]+@[\w-]+\.([\w-]+\.)?[A-Za-z]{2,8}\s*)|(((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}))$/;

@Component({
  selector: 'qa-feedback-widget',
  templateUrl: './feedback-widget.component.html',
  styleUrls: ['./feedback-widget.component.scss'],
  providers: [FeedbackWidgetService],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FeedbackWidgetComponent implements WidgetComponent {
  @Input()
  public widget!: FeedbackWidgetDetails;
  public result$?: Observable<FeedbackRequestResult>;

  public feedbackForm = new FormGroup({
    name: new FormControl('', Validators.required),
    phoneOrEmail: new FormControl('', Validators.compose([
      Validators.required,
      Validators.pattern(PHONE_OR_EMAIL)
    ])),
    text: new FormControl('', Validators.required)
  });

  constructor(private readonly feedbackWidgetService: FeedbackWidgetService) {
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
    this.result$ = this.feedbackWidgetService.send({ ...this.feedbackForm.value });
  }
}
