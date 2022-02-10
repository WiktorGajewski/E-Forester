import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IPage } from 'src/app/models/page.model';
import { ISubarea } from 'src/app/models/subarea.model';
import { SubareaService } from 'src/app/services/subareas/subarea.service';

@Component({
  selector: 'app-subarea-filter',
  templateUrl: './subarea-filter.component.html',
  styleUrls: ['./subarea-filter.component.css']
})
export class SubareaFilterComponent implements OnInit {

  subareas: ISubarea[] = [];

  @Input() selectedSubareaId: number|null = null;
  @Output() selectedSubareaIdChange = new EventEmitter<number|null>();

  private _divisionId: number|null = null;

  @Input() set divisionId(value: number|null) {

    if(this._divisionId != null) {
      this.selectedSubareaId = null;
      this.subareaIdChange();
    }

    this._divisionId = value;
    
    if(value) {
      this.load();
    }
    else {
      this.subareas = [];
    }
  }

  get divisionId() : number| null {
    return this._divisionId;
  }

  constructor(private subareaService : SubareaService) { }

  ngOnInit(): void {

  }

  load() : void {
    this.subareaService.getSubareas(null, this.divisionId, null, null)
      .subscribe({
          next: (value: IPage<ISubarea>) => {
              this.subareas = value.data;
          }
      });
  }

  subareaIdChange() : void {
    this.selectedSubareaIdChange.emit(this.selectedSubareaId);
  }
}
