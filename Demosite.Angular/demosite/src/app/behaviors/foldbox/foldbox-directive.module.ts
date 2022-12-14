import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FoldboxDirective } from './foldbox.directive';

@NgModule({
  imports: [CommonModule],
  declarations: [FoldboxDirective],
  exports: [FoldboxDirective]
})
export class FoldboxDirectiveModule {
}
