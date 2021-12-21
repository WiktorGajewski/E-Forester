import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanItemListComponent } from './plan-item-list.component';

describe('PlanItemListComponent', () => {
  let component: PlanItemListComponent;
  let fixture: ComponentFixture<PlanItemListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanItemListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanItemListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
