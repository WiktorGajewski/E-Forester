import { AfterViewInit, asNativeElements, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { IDivision } from 'src/app/models/division.model';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { IPage } from 'src/app/models/page.model';
import { ActionGroup, WoodAssortment } from 'src/app/models/plan-item.model';
import { IPlan } from 'src/app/models/plan.model';
import { ISubarea } from 'src/app/models/subarea.model';
import { DivisionService } from 'src/app/services/divisions/division.service';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';
import { PlanItemService } from 'src/app/services/plan-items/plan-item.service';
import { PlanItemsDataSource } from 'src/app/services/plan-items/plan-items.data-source';
import { PlanService } from 'src/app/services/plans/plan.service';
import { SubareaService } from 'src/app/services/subareas/subarea.service';
import { CreatePlanItemComponent } from '../create-plan-item/create-plan-item.component';

@Component({
  selector: 'app-plan-item-list',
  templateUrl: './plan-item-list.component.html',
  styleUrls: ['./plan-item-list.component.css']
})
export class PlanItemListComponent implements OnInit, AfterViewInit {
  dataSource !: PlanItemsDataSource;
  displayedColumns = ["isCompleted", "plannedHectares", "plannedCubicMeters", "woodAssortment", "actionGroup", "difficultyLevel", "factor"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  actionGroups = ActionGroup;

  plans: IPlan[] = [];
  selectedPlanId: number | undefined = undefined;

  forestUnits: IForestUnit[] = [];
  selectedForestUnitId: number | undefined;
  divisions: IDivision[] = [];
  selectedDivisionId: number | undefined;
  subareas: ISubarea[] = [];
  selectedSubareaId: number | undefined;
    
  constructor(private planItemService: PlanItemService, 
    private planService: PlanService,
    private forestUnitService: ForestUnitService,
    private divisionService: DivisionService,
    private subareaService: SubareaService,
    private dialog : MatDialog) { }

  ngOnInit(): void {
    this.dataSource = new PlanItemsDataSource(this.planItemService);
    this.dataSource.loadPlanItems();

    this.forestUnitService.getForestUnits(undefined, undefined)
      .subscribe({
          next: (value: IPage<IForestUnit>) => {
              this.forestUnits = value.data;
          }
      });
  }

  selectedForestUnit() {
    this.selectedPlanId = undefined;
    this.selectedDivisionId = undefined;
    this.selectedSubareaId = undefined;

    this.planService.getPlans(this.selectedForestUnitId, undefined, undefined)
      .subscribe({
          next: (value: IPage<IPlan>) => {
              this.plans = value.data;
          }
      });

    this.divisionService.getDivisions(this.selectedForestUnitId, undefined, undefined)
      .subscribe({
          next: (value: IPage<IDivision>) => {
              this.divisions = value.data;
          }
      });
  }

  selectedDivision() {
    this.selectedSubareaId = undefined;

    this.subareaService.getSubareas(this.selectedDivisionId, undefined, undefined)
      .subscribe({
          next: (value: IPage<ISubarea>) => {
              this.subareas = value.data;
          }
      });
  }

  ngAfterViewInit(): void {
    this.paginator.page
      .subscribe(() => this.loadPage());
  }

  loadPage() : void {
    this.dataSource.loadPlanItems(
      this.selectedForestUnitId,
      this.selectedDivisionId,
      this.selectedSubareaId,
      this.selectedPlanId,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
    );
  }

  createPlanItemDialog() : void {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = true;

    const dialogRef = this.dialog.open(CreatePlanItemComponent, dialogConfig);

    dialogRef.afterClosed().subscribe(
      result =>  {
        if(result == true) {
          this.loadPage(); 
        }
      }
    ); 
  }

  woodAssortmentsToValue(key : WoodAssortment) : string[] {

    const values : string[] = [];
    let i = 0;
    let x : number;

    while(WoodAssortment[x = 1 << i++]) {
      if(key & x) {
        values.push(WoodAssortment[x]);
      }
    }

    return values;
  }

  filter() {
    this.paginator.pageIndex = 0;
    this.loadPage();
  }
}