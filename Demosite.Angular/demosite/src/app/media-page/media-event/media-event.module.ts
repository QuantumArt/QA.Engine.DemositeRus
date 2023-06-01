import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SwiperModule } from 'swiper/angular';
import { GalleryModule } from '@ks89/angular-modal-gallery';
import { SafePipeModule } from '../../pipes';
import { MediaEventComponent } from './media-event.component';

@NgModule({
  imports: [
    CommonModule,
    SwiperModule,
    GalleryModule,
    SafePipeModule
  ],
  declarations: [MediaEventComponent],
  exports: [MediaEventComponent]
})
export class MediaEventModule {
}
