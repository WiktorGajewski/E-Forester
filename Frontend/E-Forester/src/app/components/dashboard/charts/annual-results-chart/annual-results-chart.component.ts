import { Component, OnInit } from '@angular/core';
import { ChartType } from 'chart.js';

@Component({
  selector: 'app-annual-results-chart',
  templateUrl: './annual-results-chart.component.html',
  styleUrls: ['./annual-results-chart.component.css']
})
export class AnnualResultsChartComponent implements OnInit {

  public type : ChartType = "line";

  public options = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  public legend = true;

  public labels = ["I","II","III","IV","V","VI","VII","VIII","IX","X","XI","XII"];

  public data = [
    {data: [100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100], label: "Plan"},
    {data: [2, 12, 23, 34, 46, 57, 63, 69, 75, 82, 90, 102], label: "Wykonanie"},
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
