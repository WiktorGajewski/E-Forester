import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { DivisionService } from 'src/app/services/divisions/division.service';
import { DivisionsDataSource } from 'src/app/services/divisions/divisions.data-source';
import { ForestUnitFilterComponent } from '../../filters/forest-unit-filter/forest-unit-filter.component';
import { CreateDivisionComponent } from '../create-division/create-division.component';

@Component({
  selector: 'app-division-list',
  templateUrl: './division-list.component.html',
  styleUrls: ['./division-list.component.css']
})
export class DivisionListComponent implements OnInit, AfterViewInit {
  dataSource !: DivisionsDataSource;
  displayedColumns = ["address", "area"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  @ViewChild(ForestUnitFilterComponent) forestUnitFilter !: ForestUnitFilterComponent;
  selectedForestUnitId: number | null = null;

  constructor(private divisionService : DivisionService,
    private dialog : MatDialog) {

  }

  ngOnInit(): void {
    this.dataSource = new DivisionsDataSource(this.divisionService);
    this.dataSource.loadDivisions();
  }

  ngAfterViewInit(): void {
    this.forestUnitFilter.load();

    this.paginator.page
      .subscribe(() => this.loadPage());
  }

  loadPage() : void {
    this.dataSource.loadDivisions(
      this.selectedForestUnitId,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
    );
  }

  filter() : void {
    this.paginator.pageIndex = 0;
    this.loadPage();
  }

  selectedForestUnitChange() : void {
    this.filter();
  }

  createDivisionDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreateDivisionComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.loadPage(); 
        }
      }
    ); 
  }
}