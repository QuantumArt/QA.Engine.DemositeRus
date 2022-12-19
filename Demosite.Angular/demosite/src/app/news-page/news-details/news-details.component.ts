import { ChangeDetectionStrategy, Component } from '@angular/core';
import { map, switchMap } from 'rxjs/operators';
import { NewsPageService } from '../news-page.service';
import { NewsDetailsService } from './news-details.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'qa-news-details',
  templateUrl: './news-details.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [NewsPageService, NewsDetailsService],
})
export class NewsDetailsComponent {
  public readonly newsPost$ = this.activatedRoute.params.pipe(
    switchMap(params =>
      this.newsPageService.getPageDetails().pipe(
        switchMap(({ detailstext }) => this.newsDetailsService.getNewsPost(Number(params['id']))
          .pipe(map(newsPost => ({ ...newsPost, commonText: detailstext }))))
      )
    )
  );

  constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly newsPageService: NewsPageService,
    private readonly newsDetailsService: NewsDetailsService
  ) {
  }
}
