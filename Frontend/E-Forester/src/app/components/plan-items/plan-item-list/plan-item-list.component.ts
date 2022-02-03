import { AfterViewInit, asNativeElements, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { IForestUnit } from 'src/app/models/forest-unit.model';
import { IPage } from 'src/app/models/page.model';
import { ActionGroup, WoodAssortment } from 'src/app/models/plan-item.model';
import { IPlan } from 'src/app/models/plan.model';
import { ForestUnitService } from 'src/app/services/forest-units/forest-unit.service';
import { PlanItemService } from 'src/app/services/plan-items/plan-item.service';
import { PlanItemsDataSource } from 'src/app/services/plan-items/plan-items.data-source';
import { PlanService } from 'src/app/services/plans/plan.service';
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
    
  constructor(private planItemService: PlanItemService, 
    private planService: PlanService,
    private forestUnitService: ForestUnitService,
    private dialog : MatDialog) { }

  ngOnInit(): void {
    this.dataSource = new PlanItemsDataSource(this.planItemService);
    this.dataSource.loadPlanItems();

    this.forestUnitService.getForestUnits(undefined, undefined)
      .subscribe({
          next: (value: IPage<IForestUnit>) => {
              this.forestUnits = value.data;

              if(this.forestUnits.length > 0) {
                this.selectedForestUnitId = this.forestUnits[0]?.id;
              }

              this.selectedForestUnit();
          }
      });
  }

  selectedForestUnit() {
    this.planService.getPlans(this.selectedForestUnitId, undefined, undefined)
      .subscribe({
          next: (value: IPage<IPlan>) => {
              this.plans = value.data;

              this.selectedPlanId = undefined;
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
      undefined,  //division
      undefined,  //subarea
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