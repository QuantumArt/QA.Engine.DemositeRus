import { ChangeDetectionStrategy, Component, Input, ViewEncapsulation } from '@angular/core';
import { MediaEvent, MediaEventImage } from '../media-page.service';
import { ModalGalleryService, ModalLibConfig } from '@ks89/angular-modal-gallery';

@Component({
  selector: 'qa-media-event',
  templateUrl: './media-event.component.html',
  styleUrls: ['./media-event.component.scss'],
  encapsulation: ViewEncapsulation.None,
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class MediaEventComponent {
  @Input() public item!: MediaEvent;

  constructor(private readonly modalGalleryService: ModalGalleryService) {
  }

  public openGallery(
    event: Event,
    id: number,
    sourceImages: MediaEventImage[],
    imageIndex: number,
    libConfig?: ModalLibConfig
  ): void {
    event.preventDefault();

    const images = sourceImages.map(({ id, title, image }) => ({
      id, modal: { title, img: image }
    }));
    const currentImage = images[imageIndex];
    const dialogRef = this.modalGalleryService.open({
      id,
      images,
      currentImage,
      libConfig
    });
  }
}
