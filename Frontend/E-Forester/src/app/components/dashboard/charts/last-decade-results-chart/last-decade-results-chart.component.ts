import { Component, Input, OnChanges, OnInit } from '@angular/core';
import { ChartType } from 'chart.js';

@Component({
  selector: 'last-decade-results-chart',
  templateUrl: './last-decade-results-chart.component.html',
  styleUrls: ['./last-decade-results-chart.component.css']
})
export class LastDecadeResultsChartComponent implements OnChanges {

  @Input() dataSet1 : number[] = [];
  @Input() dataLabel1 : string = "";

  @Input() dataSet2 : number[] = [];
  @Input() dataLabel2 : string = "";

  @Input() public labels : string[] = [];

  public type : ChartType = "bar";

  public options = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  public legend = true;

  public data = [
    {data: this.dataSet1, label: this.dataLabel1},
    {data: this.dataSet2, label: this.dataLabel2},
  ];

  constructor() { }

  ngOnChanges(): void {
    this.data = [
      {data: this.dataSet1, label: this.dataLabel1},
      {data: this.dataSet2, label: this.dataLabel2},
    ];
  }
}
