import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { LayoutModule } from '@angular/cdk/layout';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgChartsModule } from 'ng2-charts';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { appInitializer } from './helpers/app.intitalizer';
import { AuthService } from './services/auth/auth.service';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { JwtInterceptor } from './helpers/jwt.interceptor';

import { LoginComponent } from './components/login/login.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavComponent } from './components/nav/nav.component';
import { PlanListComponent } from './components/plans/plan-list/plan-list.component';
import { SubareaListComponent } from './components/subareas/subarea-list/subarea-list.component';
import { DivisionListComponent } from './components/divisions/division-list/division-list.component';
import { ForestUnitListComponent } from './components/forest-units/forest-unit-list/forest-unit-list.component';
import { PlanItemListComponent } from './components/plan-items/plan-item-list/plan-item-list.component';
import { PlanExecutionListComponent } from './components/plan-executions/plan-execution-list/plan-execution-list.component';
import { CreateForestUnitComponent } from './components/forest-units/create-forest-unit/create-forest-unit.component';
import { CreateDivisionComponent } from './components/divisions/create-division/create-division.component';
import { CreateSubareaComponent } from './components/subareas/create-subarea/create-subarea.component';
import { CreatePlanComponent } from './components/plans/create-plan/create-plan.component';
import { CreatePlanItemComponent } from './components/plan-items/create-plan-item/create-plan-item.component';
import { CreatePlanExecutionComponent } from './components/plan-executions/create-plan-execution/create-plan-execution.component';
import { CockpitComponent } from './components/cockpit/cockpit.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { AnnualResultsChartComponent } from './components/dashboard/charts/annual-results-chart/annual-results-chart.component';
import { LastDecadeResultsChartComponent } from './components/dashboard/charts/last-decade-results-chart/last-decade-results-chart.component';
import { EnumToArrayPipe } from './helpers/enum-to-array.pipe';

import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTableModule } from '@angular/material/table'
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { MatPaginatorIntlPl } from './localization/pl/mat-paginator-intl.pl';
import { MatSortModule } from '@angular/material/sort';
import { MatDialogModule } from '@angular/material/dialog';
import { MatGridListModule } from '@angular/material/grid-list';
import { CardComponent } from './components/dashboard/card/card.component';
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';

const material = [
  MatToolbarModule,
  MatSidenavModule,
  MatButtonModule,
  MatIconModule,
  MatListModule,
  MatFormFieldModule,
  MatCardModule,
  MatInputModule,
  MatProgressSpinnerModule,
  MatTableModule,
  MatPaginatorModule,
  MatSortModule,
  MatDialogModule,
  MatGridListModule,
  MatSelectModule,
  MatCheckboxModule
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    FooterComponent,
    NavComponent,
    PlanListComponent,
    SubareaListComponent,
    DivisionListComponent,
    ForestUnitListComponent,
    PlanItemListComponent,
    PlanExecutionListComponent,
    CreateForestUnitComponent,
    CreateDivisionComponent,
    CreateSubareaComponent,
    CreatePlanComponent,
    CreatePlanItemComponent,
    CreatePlanExecutionComponent,
    CockpitComponent,
    DashboardComponent,
    CardComponent,
    AnnualResultsChartComponent,
    LastDecadeResultsChartComponent,
    EnumToArrayPipe
  ],
  imports: [
    material,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgChartsModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializer,
      deps: [AuthService],
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    },
    {
      provide: MatPaginatorIntl,
      useClass: MatPaginatorIntlPl
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
