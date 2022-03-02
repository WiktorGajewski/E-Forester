import { Component, OnInit } from '@angular/core';
import { ChartType } from 'chart.js';

@Component({
  selector: 'app-cockpit',
  templateUrl: './cockpit.component.html',
  styleUrls: ['./cockpit.component.css']
})
export class CockpitComponent implements OnInit {

  // proper one

  chartDataTest = [
    {
      //fill: 'origin', line type
      data: [330, 600, 260, 700],
      label: 'Account A',
      backgroundColor: 'rgba(255, 255, 255, 0.2)', //bar type
      //barThickness: 30
      barPercentage: 0.7,
      categoryPercentage: 0.5,
      hoverBackgroundColor: 'rgba(0, 255, 255, 0.2)',
      grouped: 'true',
      pointStyle: 'cross',
      stacked: true,
      legend: true

    },
    {
      //fill: 'origin', line type
      data: [120, 455, 100, 340],
      label: 'Account B',
      barPercentage: 0.7,
      categoryPercentage: 0.5,
      stacked: true,
      stack: 'a' 

    },
    {
      fill: {
        target: '-1',
        above: 'rgb(100, 0, 0)',   // Area will be red above the origin
        below: 'rgb(0, 100, 0)'    // And blue below the origin
      },
      data: [45, 67, 800, 500],
      label: 'Account C',
      barPercentage: 0.7,
      categoryPercentage: 0.5,
      stacked: true,
      stack: 'a' 

    }
  ];

  chartLabelsTest = [
    'January',
    'February',
    'March',
    'April'
  ];

  chartOptionsTest = {
    responsive: true,
    scaleShowVerticalLines: false
  };

  //

  public barChartOptions = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  public barChartLabels = ["2011","2012","2013","2014","2015","2016","2017"];
  public barChartType : ChartType = "bar";
  public doughnutChartType : ChartType = "doughnut";
  public radarChartType : ChartType = "radar";
  public pieChartType : ChartType = "pie";
  public scatterChartType : ChartType = "scatter";
  public lineChartType : ChartType = "line";


  public barChartLegend = true;

  public barChartData = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: "Series A"},
    {data: [28, 48, 40, 19, 36, 57, 20], label: "Series B"},
  ];

  public chartData = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: "Example"},
  ];

  public scatterData = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: "Example"},
    {data: [59, 70, 81], label: "Example"},
    {data: [100], label: "Example"}
  ];

  public combinedChart = [
    { "data": [0, 30, 20, 40, 35, 45, 33, 0, 0], "label": "Bar 1", "type": "bar", "order": 2},
    { "data": [0, 50, 60, 55, 59, 30, 40, 0, 0], "label": "Bar 2", "type": "bar", "order": 2 },
    { "data": [45, 45, 45, 55, 45, 65, 45, 45, 45], "label": "Line", "type": "line", "order": 1 }
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
