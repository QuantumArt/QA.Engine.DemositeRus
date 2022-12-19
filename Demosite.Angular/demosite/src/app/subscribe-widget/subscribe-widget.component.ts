import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { WidgetComponent, WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';
import { SubscribeWidgetService } from './subscribe-widget.service';

export interface SubscribeWidgetDetails extends WidgetDetails {
}

@Component({
  selector: 'qa-subscribe-widget',
  templateUrl: './subscribe-widget.component.html',
  styleUrls: ['./subscribe-widget.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [SubscribeWidgetService]
})
export class SubscribeWidgetComponent implements WidgetComponent {
  @Input()
  public widget!: SubscribeWidgetDetails;

  public subscribeForm = new FormGroup({
    gender: new FormControl('', Validators.required),
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    company: new FormControl('', Validators.required),
    email: new FormControl('', Validators.compose([
      Validators.required,
      Validators.email
    ]))
  });

  public categories$ = this.subscribeWidgetService.getCategories();

  constructor(private readonly subscribeWidgetService: SubscribeWidgetService) {
  }

  public get name() {
    return this.subscribeForm.get('name')!;
  }

  public get phoneOrEmail() {
    return this.subscribeForm.get('phoneOrEmail')!;
  }

  public get text() {
    return this.subscribeForm.get('text')!;
  }

  public onSubmit(): void {
    //TODO: send form
    //this.http.post()
  }
}
