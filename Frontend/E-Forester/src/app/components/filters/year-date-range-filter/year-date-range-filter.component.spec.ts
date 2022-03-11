import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YearDateRangeFilterComponent } from './year-date-range-filter.component';

describe('YearDateRangeFilterComponent', () => {
  let component: YearDateRangeFilterComponent;
  let fixture: ComponentFixture<YearDateRangeFilterComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YearDateRangeFilterComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(YearDateRangeFilterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
