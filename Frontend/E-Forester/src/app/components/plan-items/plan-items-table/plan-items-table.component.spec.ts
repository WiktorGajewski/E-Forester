import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanItemsTableComponent } from './plan-items-table.component';

describe('PlanItemsTableComponent', () => {
  let component: PlanItemsTableComponent;
  let fixture: ComponentFixture<PlanItemsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanItemsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanItemsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
