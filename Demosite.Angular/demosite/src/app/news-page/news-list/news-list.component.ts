import { ChangeDetectionStrategy, Component, ElementRef, TrackByFunction, ViewChild } from '@angular/core';
import { NewsPageService } from '../news-page.service';
import { NewsListService, NewsPost } from './news-list.service';
import { map, switchMap } from 'rxjs/operators';
import { BehaviorSubject } from 'rxjs';

export interface NewsFilter {
  year: number;
  month: number;
}

@Component({
  selector: 'qa-news-list',
  templateUrl: './news-list.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [NewsPageService, NewsListService],
})
export class NewsListComponent {
  @ViewChild('month') public readonly monthEl?: ElementRef<HTMLSelectElement>;

  public readonly pageDetails$ = this.newsPageService.getPageDetails().pipe(
    switchMap(details => this.newsListService.getNewsPosts(details.categoryid).pipe(
      map(items => ({ details, years: this.getYearsList(items), items })))
    ),
    switchMap(({ details, years, items }) => this.filter$.pipe(
      map(filter => ({ details, years, items: this.filter(items, filter) })))
    ),
  );

  public readonly trackById: TrackByFunction<NewsPost> = (_, { id }) => id;

  private readonly filter$ = new BehaviorSubject<NewsFilter>({ year: 0, month: 0 });

  constructor(
    private readonly newsPageService: NewsPageService,
    private readonly newsListService: NewsListService
  ) {
  }

  public changeFilter(year: string, month?: string): void {
    if (!month && this.monthEl?.nativeElement) {
      this.monthEl.nativeElement.value = '0';
    }
    this.filter$.next({ year: Number(year), month: Number(month ?? 0) });
  }

  private getYearsList(items: NewsPost[]): number[] {
    const years = items.map(({ postDate }) => new Date(postDate).getFullYear());
    years.sort((a, b) => b - a);

    return Array.from(new Set<number>(years).values());
  }

  private filter(items: NewsPost[], filter: NewsFilter): NewsPost[] {
    return items.filter(({ postDate }) => {
      const date = new Date(postDate);

      return (filter.year === 0 || date.getFullYear() === filter.year) &&
        (filter.month === 0 || date.getMonth() + 1 === filter.month);
    });
  }
}
