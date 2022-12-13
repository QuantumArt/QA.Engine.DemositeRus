import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  selector: 'qa-redirect-page',
  template: '<!-- Redirecting... -->',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RedirectPageComponent {
}
