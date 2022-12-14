import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogDirective } from './dialog.directive';
import { ContentComponent } from './content.component';

@NgModule({
  imports: [CommonModule, MatDialogModule],
  declarations: [DialogDirective, ContentComponent],
  entryComponents: [ContentComponent],
  exports: [DialogDirective]
})
export class DialogDirectiveModule {
}
