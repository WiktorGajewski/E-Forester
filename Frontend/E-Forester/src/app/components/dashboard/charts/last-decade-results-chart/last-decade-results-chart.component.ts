import { Component, OnInit } from '@angular/core';
import { ChartType } from 'chart.js';

@Component({
  selector: 'last-decade-results-chart',
  templateUrl: './last-decade-results-chart.component.html',
  styleUrls: ['./last-decade-results-chart.component.css']
})
export class LastDecadeResultsChartComponent implements OnInit {
  public type : ChartType = "bar";

  public options = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  public legend = true;

  public labels = ["2011","2012","2013","2014","2015","2016","2017"];

  public data = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: "Plan"},
    {data: [28, 48, 40, 19, 36, 57, 20], label: "Wykonanie"},
  ];

  constructor() { }

  ngOnInit(): void {
  }
}
