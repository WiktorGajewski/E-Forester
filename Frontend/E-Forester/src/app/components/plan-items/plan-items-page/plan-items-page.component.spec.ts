import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlanItemsPageComponent } from './plan-items-page.component';

describe('PlanItemsPageComponent', () => {
  let component: PlanItemsPageComponent;
  let fixture: ComponentFixture<PlanItemsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlanItemsPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlanItemsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
