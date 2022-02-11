import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePlanExecutionComponent } from './create-plan-execution.component';

describe('CreatePlanExecutionComponent', () => {
  let component: CreatePlanExecutionComponent;
  let fixture: ComponentFixture<CreatePlanExecutionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreatePlanExecutionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePlanExecutionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
