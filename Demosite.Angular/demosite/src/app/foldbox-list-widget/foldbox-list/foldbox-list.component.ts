import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { FoldboxListItem } from '../foldbox-list-widget.service';

@Component({
  selector: 'qa-foldbox-list',
  templateUrl: './foldbox-list.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FoldboxListComponent {
  @Input() public items!: FoldboxListItem[] | null;

  public opened = false;
}
