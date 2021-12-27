import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LastDecadeResultsChartComponent } from './last-decade-results-chart.component';

describe('LastDecadeResultsChartComponent', () => {
  let component: LastDecadeResultsChartComponent;
  let fixture: ComponentFixture<LastDecadeResultsChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LastDecadeResultsChartComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LastDecadeResultsChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
