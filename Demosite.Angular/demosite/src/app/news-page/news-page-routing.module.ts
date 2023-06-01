import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewsDetailsComponent } from './news-details';
import { NewsListComponent } from './news-list';
import { NewsPageComponent } from './news-page.component';

const routes: Routes = [
  {
    path: '',
    component: NewsPageComponent,
    children: [
      {
        path: 'details/:id',
        component: NewsDetailsComponent
      },
      {
        path: '',
        component: NewsListComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class NewsPageRoutingModule {
}
