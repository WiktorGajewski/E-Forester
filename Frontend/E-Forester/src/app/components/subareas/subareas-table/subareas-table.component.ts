import { AfterViewInit, Component, Input, OnChanges, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { SubareasDataSource } from 'src/app/services/subareas/subareas.data-source';
import { SubareaService } from 'src/app/services/subareas/subarea.service';

@Component({
  selector: 'app-subareas-table',
  templateUrl: './subareas-table.component.html',
  styleUrls: ['./subareas-table.component.css']
})
export class SubareasTableComponent implements OnInit, AfterViewInit, OnChanges {
  dataSource !: SubareasDataSource;
  displayedColumns = ["address", "area"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  @Input() selectedForestUnitId: number | null = null;

  @Input() selectedDivisionId: number | null = null;

  constructor(private subareaService : SubareaService) { }
  
  ngOnInit(): void {
    this.dataSource = new SubareasDataSource(this.subareaService);
  }

  ngAfterViewInit(): void {

    this.paginator.page
      .subscribe(() => this.reloadTable());
  }

  ngOnChanges(): void {
    if(this.dataSource){
      this.paginator.pageIndex = 0;
      this.reloadTable();
    }
  }

  reloadTable() : void {
    this.dataSource.loadSubareas(
      this.selectedForestUnitId,
      this.selectedDivisionId,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
    );
  }
}
