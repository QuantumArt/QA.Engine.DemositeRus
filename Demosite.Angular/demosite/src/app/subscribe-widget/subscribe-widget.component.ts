import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { WidgetComponent, WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';
import { NewsCategory, SubscribeWidgetService } from './subscribe-widget.service';
import { tap } from 'rxjs';

export interface SubscribeWidgetDetails extends WidgetDetails {
}

export interface Gender {
  title: 'Уважаемый' | 'Уважаемая';
  value: 'm' | 'f';
  checked: boolean;
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

  public readonly genders: Gender[] = [{
    title: 'Уважаемый',
    value: 'm',
    checked: true
  }, {
    title: 'Уважаемая',
    value: 'f',
    checked: false
  }];

  public subscribeForm = new FormGroup({
    gender: new FormControl('', Validators.required),
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    company: new FormControl('', Validators.required),
    email: new FormControl('', Validators.compose([
      Validators.required,
      Validators.email
    ])),
    categories: new FormArray([])
  });

  public categories$ = this.subscribeWidgetService.getCategories().pipe(
    tap(categories => {
      categories.forEach(category => this.categories.push(new FormControl(true)))
    })
  );

  constructor(private readonly subscribeWidgetService: SubscribeWidgetService) {
  }

  public get gender() {
    return this.subscribeForm.get('gender')!;
  }

  public get firstName() {
    return this.subscribeForm.get('firstName')!;
  }

  public get lastName() {
    return this.subscribeForm.get('lastName')!;
  }

  public get company() {
    return this.subscribeForm.get('company')!;
  }

  public get email() {
    return this.subscribeForm.get('email')!;
  }

  public get categories() {
    return this.subscribeForm.get('categories')! as FormArray;
  }

  public onSubmit(categories: NewsCategory[]): void {
    //TODO: send form
    //this.http.post()
    console.log(this.subscribeForm.value);
  }
}
