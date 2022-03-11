import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AnnualResultsChartComponent } from './annual-results-chart.component';

describe('AnnualResultsChartComponent', () => {
  let component: AnnualResultsChartComponent;
  let fixture: ComponentFixture<AnnualResultsChartComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AnnualResultsChartComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AnnualResultsChartComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
