import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FoldboxTabsComponent } from './foldbox-tabs.component';
import { SafePipeModule } from '../../pipes';
import { TabsDirectiveModule } from '../../behaviors';

@NgModule({
  imports: [CommonModule, SafePipeModule, TabsDirectiveModule],
  declarations: [FoldboxTabsComponent],
  exports: [FoldboxTabsComponent]
})
export class FoldboxTabsModule {
}
