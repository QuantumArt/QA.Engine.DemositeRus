import { ChangeDetectionStrategy, Component, Input, TrackByFunction } from '@angular/core';
import { FoldboxListItem } from '../foldbox-list-widget.service';

@Component({
  selector: 'qa-foldbox-list',
  templateUrl: './foldbox-list.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class FoldboxListComponent {
  @Input() public items!: FoldboxListItem[] | null;

  public readonly trackById: TrackByFunction<FoldboxListItem> = (_, item) => item.id;
  public opened = false;
}
