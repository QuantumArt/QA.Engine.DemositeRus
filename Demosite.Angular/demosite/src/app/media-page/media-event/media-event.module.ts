import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GalleryModule } from '@ks89/angular-modal-gallery';
import { SwiperModule } from 'swiper/angular';
import { SafePipeModule } from '../../pipes';
import { FoldboxDirectiveModule } from '../../behaviors';
import { MediaEventComponent } from './media-event.component';

@NgModule({
  imports: [
    CommonModule,
    SwiperModule,
    GalleryModule,
    SafePipeModule,
    FoldboxDirectiveModule
  ],
  declarations: [MediaEventComponent],
  exports: [
    MediaEventComponent
  ]
})
export class MediaEventModule {
}
