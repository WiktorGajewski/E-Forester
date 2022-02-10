import { AfterViewInit, Component, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { ActionGroup, WoodAssortment } from 'src/app/models/plan-item.model';
import { PlanItemService } from 'src/app/services/plan-items/plan-item.service';
import { PlanItemsDataSource } from 'src/app/services/plan-items/plan-items.data-source';

@Component({
  selector: 'app-plan-items-table',
  templateUrl: './plan-items-table.component.html',
  styleUrls: ['./plan-items-table.component.css']
})
export class PlanItemsTableComponent implements OnInit, AfterViewInit, OnChanges {
  dataSource !: PlanItemsDataSource;
  displayedColumns = ["isCompleted", "plannedHectares", "plannedCubicMeters", "woodAssortment", "actionGroup", "difficultyLevel", "factor"];

  @ViewChild(MatPaginator) paginator !: MatPaginator;

  @Input() selectedForestUnitId: number | null = null;
  @Input() selectedDivisionId: number | null = null;
  @Input() selectedSubareaId: number | null = null;
  @Input() selectedPlanId: number | null = null;

  actionGroups = ActionGroup;
    
  constructor(private planItemService: PlanItemService) {}

  ngOnInit(): void {
    this.dataSource = new PlanItemsDataSource(this.planItemService);
  }

  ngAfterViewInit(): void {

    this.paginator.page
      .subscribe(() => this.reloadTable());
  }

  ngOnChanges(changes: SimpleChanges): void {

    if(this.dataSource){
      this.reloadTable();
    }
  }

  reloadTable() : void {
    this.dataSource.loadPlanItems(
      this.selectedForestUnitId,
      this.selectedDivisionId,
      this.selectedSubareaId,
      this.selectedPlanId,
      this.paginator.pageIndex + 1,
      this.paginator.pageSize
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
}