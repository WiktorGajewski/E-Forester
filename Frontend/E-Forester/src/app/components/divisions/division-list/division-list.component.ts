import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { Role } from 'src/app/models/user.model';
import { AuthService } from 'src/app/services/auth/auth.service';
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

  selectedForestUnitId: number | null = null;

  userRole: Role | undefined;

  constructor(private divisionService : DivisionService,
    private dialog : MatDialog,
    private authService: AuthService) {

      this.authService.authentication.subscribe(auth => 
        {
          this.userRole = auth?.userRole;
        });
  }

  ngOnInit(): void {
    this.dataSource = new DivisionsDataSource(this.divisionService);
  }

  ngAfterViewInit(): void {

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