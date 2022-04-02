import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-mini-card',
  templateUrl: './mini-card.component.html',
  styleUrls: ['./mini-card.component.css']
})
export class MiniCardComponent {
  @Input() title : string | undefined;
  @Input() value : number | undefined;
  @Input() unit : string | undefined;

  constructor() { }
}
