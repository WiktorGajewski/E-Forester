import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanExecutionListComponent } from './plan-execution-list.component';

describe('PlanExecutionListComponent', () => {
  let component: PlanExecutionListComponent;
  let fixture: ComponentFixture<PlanExecutionListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanExecutionListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanExecutionListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
