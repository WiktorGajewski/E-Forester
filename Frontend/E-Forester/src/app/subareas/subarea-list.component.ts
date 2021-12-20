import { Component, OnInit } from '@angular/core';
import { SubareaDataSource } from '../services/subareas/subarea.data-source';
import { SubareaService } from '../services/subareas/subarea.service';

@Component({
  selector: 'app-subarea-list',
  templateUrl: './subarea-list.component.html',
  styleUrls: ['./subarea-list.component.css']
})
export class SubareaListComponent implements OnInit {
  dataSource !: SubareaDataSource;
  displayedColumns = ["id", "address", "area"];

  constructor(private subareaService : SubareaService) { 

  }

  ngOnInit(): void {
    this.dataSource = new SubareaDataSource(this.subareaService);
    this.dataSource.loadSubareas();
  }

}
