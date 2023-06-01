import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FoldboxListComponent } from './foldbox-list.component';
import { FoldboxDirectiveModule } from '../../behaviors';
import { SafePipeModule } from '../../pipes';

@NgModule({
  imports: [CommonModule, FoldboxDirectiveModule, SafePipeModule],
  declarations: [FoldboxListComponent],
  exports: [FoldboxListComponent]
})
export class FoldboxListModule {
}
