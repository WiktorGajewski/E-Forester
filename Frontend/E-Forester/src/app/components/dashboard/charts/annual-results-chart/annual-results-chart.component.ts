import { Component, Input, OnChanges } from '@angular/core';
import { ChartType } from 'chart.js';

@Component({
  selector: 'annual-results-chart',
  templateUrl: './annual-results-chart.component.html',
  styleUrls: ['./annual-results-chart.component.css']
})
export class AnnualResultsChartComponent implements OnChanges {
  @Input() title : string | undefined;

  @Input() dataSet1 : number[] = [];
  @Input() dataLabel1 : string = "";

  @Input() dataSet2 : number[] = [];
  @Input() dataLabel2 : string = "";

  @Input() public labels : string[] = [];

  chartType : ChartType = "bar"

  public options = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  public legend = true;

  public data = [
    {
      data: this.dataSet1, 
      label: this.dataLabel1,
      categoryPercentage: 0.3,
      backgroundColor: 'rgba(217, 111, 50, 0.9)',
      hoverBackgroundColor: 'rgba(140, 77, 39, 1)',
      borderColor: 'rgb(180, 90, 70)',
      fill: {
        target: '1',
        above: 'rgb(80, 30, 20)',
        below: 'rgb(0, 80, 40)'
      },
    },
    {
      data: this.dataSet2, 
      label: this.dataLabel2,
      categoryPercentage: 0.3,
      backgroundColor: 'rgba(46, 139, 87, 0.9)',
      hoverBackgroundColor: 'rgba(34, 139, 34, 1)',
      borderColor: 'rgb(34, 180, 34)'
    }
  ];

  constructor() { }

  ngOnChanges(): void {

    this.data = [
      {
        data: this.dataSet1, 
        label: this.dataLabel1,
        categoryPercentage: 0.3,
        backgroundColor: 'rgba(217, 111, 50, 0.9)',
        hoverBackgroundColor: 'rgba(140, 77, 39, 1)',
        borderColor: 'rgb(180, 90, 70)',
        fill: {
          target: '1',
          above: 'rgb(80, 30, 20)',
          below: 'rgb(0, 80, 40)'
        }
      },
      {
        data: this.dataSet2, 
        label: this.dataLabel2,
        categoryPercentage: 0.3,
        backgroundColor: 'rgba(46, 139, 87, 0.9)',
        hoverBackgroundColor: 'rgba(34, 139, 34, 1)',
        borderColor: 'rgb(34, 180, 34)'
      }
    ];
  }

  changeChartType(): void {
    if(this.chartType === "bar") {
      this.chartType = "line";
    }
    else {
      this.chartType = "bar";
    }
  }
}
