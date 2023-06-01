import { Component, Inject, ViewEncapsulation } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface DialogData {
  content: string;
}

@Component({
  selector: 'qa-content',
  templateUrl: './content.component.html',
  styleUrls: ['./content.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ContentComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public readonly data: DialogData) {
  }
}
