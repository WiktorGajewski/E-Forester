import { Component, OnInit } from '@angular/core';
import { ChartType } from 'chart.js';

@Component({
  selector: 'app-cockpit',
  templateUrl: './cockpit.component.html',
  styleUrls: ['./cockpit.component.css']
})
export class CockpitComponent implements OnInit {

  public barChartOptions = {
    scaleShowVerticalLines: false,
    responsive: true
  };

  public barChartLabels = ["2011","2012","2013","2014","2015","2016","2017"];
  public barChartType : ChartType = "bar";
  public doughnutChartType : ChartType = "doughnut";
  public radarChartType : ChartType = "radar";
  public pieChartType : ChartType = "pie";

  public barChartLegend = true;

  public barChartData = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: "Series A"},
    {data: [28, 48, 40, 19, 36, 57, 20], label: "Series B"},
  ];

  public chartData = [
    {data: [65, 59, 80, 81, 56, 55, 40], label: "Example"},
  ];

  constructor() { }

  ngOnInit(): void {
  }

}
