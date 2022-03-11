import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanExecutionsTableComponent } from './plan-executions-table.component';

describe('PlanExecutionsTableComponent', () => {
  let component: PlanExecutionsTableComponent;
  let fixture: ComponentFixture<PlanExecutionsTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanExecutionsTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanExecutionsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
