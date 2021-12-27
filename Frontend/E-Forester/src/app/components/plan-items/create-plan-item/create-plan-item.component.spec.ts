import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreatePlanItemComponent } from './create-plan-item.component';

describe('CreatePlanItemComponent', () => {
  let component: CreatePlanItemComponent;
  let fixture: ComponentFixture<CreatePlanItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreatePlanItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreatePlanItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
