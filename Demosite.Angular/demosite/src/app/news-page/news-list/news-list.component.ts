import {
  ChangeDetectionStrategy,
  Component,
  ElementRef,
  TrackByFunction,
  ViewChild,
} from '@angular/core';
import { BehaviorSubject, combineLatest } from 'rxjs';
import { map, switchMap } from 'rxjs/operators';
import { NewsPageService } from '../news-page.service';
import { NewsListService, NewsPost } from './news-list.service';

const ITEMS_PER_PAGE = 2;

export interface NewsFilter {
  year: number;
  month: number;
}

@Component({
  selector: 'qa-news-list',
  templateUrl: './news-list.component.html',
  styleUrls: ['./news-list.components.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [NewsPageService, NewsListService],
})
export class NewsListComponent {
  @ViewChild('month') public readonly monthEl?: ElementRef<HTMLSelectElement>;

  public readonly months = [
    [0, 'Все'],
    [1, 'Январь'],
    [2, 'Февраль'],
    [3, 'Март'],
    [4, 'Апрель'],
    [5, 'Май'],
    [6, 'Июнь'],
    [7, 'Июль'],
    [8, 'Август'],
    [9, 'Сентябрь'],
    [10, 'Октябрь'],
    [11, 'Ноябрь'],
    [12, 'Декабрь'],
  ];

  public readonly currentPage$ = new BehaviorSubject<number>(1);
  public readonly pageDetails$ = this.newsPageService.getPageDetails().pipe(
    switchMap((details) =>
      this.newsListService
        .getNewsPosts(details.categoryid)
        .pipe(
          map((items) => ({ details, years: this.getYearsList(items), items }))
        )
    ),
    switchMap(({ details, years, items }) =>
      combineLatest([this.filter$, this.currentPage$]).pipe(
        map(([filter, currentPage]) => {
          const filteredItems = this.filter(items, filter);
          const paginatedItems = this.paginate(filteredItems, currentPage);

          return {
            details,
            years,
            items: paginatedItems,
            pages: this.getPages(filteredItems.length),
          }
        })
      )
    )
  );

  public readonly trackById: TrackByFunction<NewsPost> = (_, { id }) => id;

  private readonly filter$ = new BehaviorSubject<NewsFilter>({
    year: 0,
    month: 0,
  });

  constructor(
    private readonly newsPageService: NewsPageService,
    private readonly newsListService: NewsListService
  ) {}

  public getPages(itemsCount: number): number[] {
    const count = Math.ceil(itemsCount / ITEMS_PER_PAGE);
    const result = [];

    for (let i = 1; i <= count; i++) {
      result.push(i);
    }

    return result;
  }

  public changeFilter(year: string, month?: string): void {
    if (!month && this.monthEl?.nativeElement) {
      this.monthEl.nativeElement.value = '0';
    }
    this.filter$.next({
      year: Number(year),
      month: Number(month ?? 0),
    });
    this.currentPage$.next(1);
  }

  public changePage(page: number, total: number, event: MouseEvent): void {
    event.preventDefault();

    if (page > total || page < 1) {
      return;
    }

    this.currentPage$.next(page);
  }

  private getYearsList(items: NewsPost[]): number[] {
    const years = items.map(({ postDate }) => new Date(postDate).getFullYear());
    years.sort((a, b) => b - a);

    return Array.from(new Set<number>(years).values());
  }

  private filter(items: NewsPost[], filter: NewsFilter): NewsPost[] {
    return items.filter(({ postDate }) => {
      const date = new Date(postDate);

      return (
        (filter.year === 0 || date.getFullYear() === filter.year) &&
        (filter.month === 0 || date.getMonth() + 1 === filter.month)
      );
    });
  }

  private paginate(items: NewsPost[], currentPage: number): NewsPost[] {
    const firstIndex = currentPage === 1 ? 0 : (currentPage - 1) * ITEMS_PER_PAGE;
    const lastIndex = firstIndex + 2;

    return items.filter((item: NewsPost, index: number) => {
      if (index < firstIndex || index >= lastIndex) {
        return false;
      }

      return true;
    })
  }
}
