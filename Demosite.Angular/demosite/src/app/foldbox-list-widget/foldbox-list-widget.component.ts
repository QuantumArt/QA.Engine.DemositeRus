import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { Observable } from 'rxjs';
import { WidgetComponent, WidgetDetails } from '@quantumart/qa-engine-page-structure-angular';
import { FoldboxListItem, FoldboxListWidgetService } from './foldbox-list-widget.service';

export interface FoldboxListWidgetDetails extends WidgetDetails {
  widgettype?: string;
  foldboxlistitems: number[];
}

@Component({
  selector: 'qa-foldbox-list-widget',
  templateUrl: './foldbox-list-widget.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [FoldboxListWidgetService]
})
export class FoldboxListWidgetComponent implements WidgetComponent {
  @Input()
  public set widget(widget: FoldboxListWidgetDetails) {
    this.title = widget.title;
    this.widgetType = widget.widgettype;

    if (widget.foldboxlistitems) {
      this.items$ = this.foldboxListWidgetService.getBanners(widget.foldboxlistitems);
    }
  }

  public title?: string;
  public widgetType?: string;
  public items$?: Observable<FoldboxListItem[]>;

  constructor(private readonly foldboxListWidgetService: FoldboxListWidgetService) {
  }
}
