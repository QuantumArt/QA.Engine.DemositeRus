import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MediaPageComponent } from './media-page.component';

const routes: Routes = [
  {
    path: '',
    component: MediaPageComponent,
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MediaPageRoutingModule {
}
