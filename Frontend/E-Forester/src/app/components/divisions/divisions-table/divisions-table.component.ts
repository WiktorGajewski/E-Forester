import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { DivisionService } from 'src/app/services/divisions/division.service';
import { DivisionsDataSource } from 'src/app/services/divisions/divisions.data-source';

@Component({
  selector: 'app-divisions-table',
  templateUrl: './divisions-table.component.html',
  styleUrls: ['./divisions-table.component.css']
})
export class DivisionsTableComponent implements OnInit, AfterViewInit, OnChanges {
  dataSource !: DivisionsDataSource;
  displayedColumns = ["address", "area"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  @Input() selectedForestUnitId: number | null = null;

  constructor(private divisionService : DivisionService) { }

  ngOnInit(): void {
    this.dataSource = new DivisionsDataSource(this.divisionService);
  }

  ngAfterViewInit(): void {

    this.paginator.page
      .subscribe(() => this.reloadTable());
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(this.dataSource){
      this.paginator.pageIndex = 0;
      this.reloadTable();
    }
  }

  reloadTable() : void {
    this.dataSource.loadDivisions(
      this.selectedForestUnitId,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
    );
  }
}