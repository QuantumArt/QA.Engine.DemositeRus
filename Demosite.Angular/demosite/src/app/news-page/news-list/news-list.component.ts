import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { NewsPageService } from '../news-page.service';
import { NewsListService, NewsPost } from './news-list.service';

@Component({
  selector: 'qa-news-list',
  templateUrl: './news-list.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [NewsPageService, NewsListService],
})
export class NewsListComponent {
  public readonly pageDetails$ = this.newsPageService.getPageDetails().pipe(
    tap(({ categoryid }) => {
      this.items$ = this.newsListService.getNewsPosts(categoryid);
    })
  );
  public items$?: Observable<NewsPost[]>;

  constructor(
    private readonly newsPageService: NewsPageService,
    private readonly newsListService: NewsListService
  ) {
  }
}
