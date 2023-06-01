import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule } from '@angular/material/dialog';
import { DialogDirective } from './dialog.directive';
import { ContentComponent } from './content.component';
import { SafePipeModule } from '../../pipes';

@NgModule({
    imports: [CommonModule, MatDialogModule, SafePipeModule],
  declarations: [DialogDirective, ContentComponent],
  entryComponents: [ContentComponent],
  exports: [DialogDirective]
})
export class DialogDirectiveModule {
}
