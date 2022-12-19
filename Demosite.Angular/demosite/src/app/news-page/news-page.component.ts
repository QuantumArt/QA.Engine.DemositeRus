import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NewsPageService } from './news-page.service';

@Component({
  selector: 'qa-news-page',
  templateUrl: './news-page.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [NewsPageService]
})
export class NewsPageComponent {
  public readonly pageDetails$ = this.newsPageService.getPageDetails();

  constructor(private readonly newsPageService: NewsPageService) {
  }
}
