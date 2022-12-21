import { ChangeDetectionStrategy, Component, Input, ViewEncapsulation } from '@angular/core';
import { MediaEvent, MediaEventImage } from '../media-page.service';
import {
  ButtonsConfig,
  ButtonsStrategy,
  ButtonType, DescriptionStrategy,
  ModalGalleryService,
  ModalLibConfig
} from '@ks89/angular-modal-gallery';
import SwiperCore, { Navigation } from 'swiper';

const GALLERY_BUTTONS: ButtonsConfig = {
  visible: true,
  strategy: ButtonsStrategy.CUSTOM,
  buttons: [
    {
      type: ButtonType.FULLSCREEN,
      title: 'Switch to full-screen',
      ariaLabel: 'Switch to full-screen',
      className: 'fullscreen-image'
    },
    {
      type: ButtonType.CLOSE,
      title: 'Close this modal image gallery',
      ariaLabel: 'Close this modal image gallery',
      className: 'close-image'
    }
  ]
};

SwiperCore.use([Navigation]);

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
    imageIndex: number
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
      libConfig: {
        buttonsConfig: GALLERY_BUTTONS,
        slideConfig: {
          infinite: true,
          sidePreviews: {
            show: false
          }
        },
        currentImageConfig: {
          description: {
            strategy: DescriptionStrategy.ALWAYS_VISIBLE,
            imageText: '',
            numberSeparator: ' из ',
            beforeTextDescription: ' => '
          }
        }
      } as ModalLibConfig
    });
  }
}
