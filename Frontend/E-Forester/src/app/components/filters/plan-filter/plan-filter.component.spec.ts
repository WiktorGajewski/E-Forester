import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanFilterComponent } from './plan-filter.component';

describe('PlanFilterComponent', () => {
  let component: PlanFilterComponent;
  let fixture: ComponentFixture<PlanFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
