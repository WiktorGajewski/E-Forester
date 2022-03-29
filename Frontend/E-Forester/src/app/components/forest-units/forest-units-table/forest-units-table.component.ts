import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';
import { ForestUnitsDataSource } from 'src/app/services/forest-units/forest-units.data-source';

@Component({
  selector: 'app-forest-units-table',
  templateUrl: './forest-units-table.component.html',
  styleUrls: ['./forest-units-table.component.css']
})
export class ForestUnitsTableComponent implements OnInit, AfterViewInit {
  dataSource !: ForestUnitsDataSource;
  displayedColumns = ["name", "address", "area"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  constructor(private forestUnitService : ForestUnitService) {  }

  ngOnInit(): void {
    this.dataSource = new ForestUnitsDataSource(this.forestUnitService);
    this.dataSource.loadForestUnits();
  }

  ngAfterViewInit(): void {
    this.paginator.page
      .subscribe(() => this.reloadTable());
  }

  reloadTable() : void {
    this.dataSource.loadForestUnits(
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
    );
  }
}