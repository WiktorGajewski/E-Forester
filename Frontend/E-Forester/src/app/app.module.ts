import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { LayoutModule } from '@angular/cdk/layout';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgChartsModule } from 'ng2-charts';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { appInitializer } from './helpers/app.intitalizer';
import { AuthService } from './services/auth/auth.service';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { JwtInterceptor } from './helpers/jwt.interceptor';

import { LoginComponent } from './components/login/login.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavComponent } from './components/nav/nav.component';
import { PlansPageComponent } from './components/plans/plans-page/plans-page.component';
import { PlansTableComponent } from './components/plans/plans-table/plans-table.component';
import { SubareasPageComponent } from './components/subareas/subareas-page/subareas-page.component';
import { SubareasTableComponent } from './components/subareas/subareas-table/subareas-table.component';
import { DivisionsPageComponent } from './components/divisions/divisions-page/divisions-page.component';
import { DivisionsTableComponent } from './components/divisions/divisions-table/divisions-table.component';
import { ForestUnitsPageComponent } from './components/forest-units/forest-units-page/forest-units-page.component';
import { ForestUnitsTableComponent } from './components/forest-units/forest-units-table/forest-units-table.component';
import { PlanItemsPageComponent } from './components/plan-items/plan-items-page/plan-items-page.component';
import { PlanItemsTableComponent } from './components/plan-items/plan-items-table/plan-items-table.component';
import { PlanExecutionsTableComponent } from './components/plan-executions/plan-executions-table/plan-executions-table.component';
import { CreateForestUnitComponent } from './components/forest-units/create-forest-unit/create-forest-unit.component';
import { CreateDivisionComponent } from './components/divisions/create-division/create-division.component';
import { CreateSubareaComponent } from './components/subareas/create-subarea/create-subarea.component';
import { CreatePlanComponent } from './components/plans/create-plan/create-plan.component';
import { CreatePlanItemComponent } from './components/plan-items/create-plan-item/create-plan-item.component';
import { CreatePlanExecutionComponent } from './components/plan-executions/create-plan-execution/create-plan-execution.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { MiniCardComponent } from './components/dashboard/mini-card/mini-card.component';
import { AnnualResultsChartComponent } from './components/dashboard/charts/annual-results-chart/annual-results-chart.component';
import { EnumToArrayPipe } from './helpers/pipes/enum-to-array.pipe';
import { ForestUnitFilterComponent } from './components/filters/forest-unit-filter/forest-unit-filter.component';
import { DivisionFilterComponent } from './components/filters/division-filter/division-filter.component';
import { SubareaFilterComponent } from './components/filters/subarea-filter/subarea-filter.component';
import { PlanFilterComponent } from './components/filters/plan-filter/plan-filter.component';
import { YearDateRangeFilterComponent } from './components/filters/year-date-range-filter/year-date-range-filter.component';
import { UsersPageComponent } from './components/users/users-page/users-page.component';
import { UsersTableComponent } from './components/users/users-table/users-table.component';
import { CreateUserComponent } from './components/users/create-user/create-user.component';
import { AssignForestUnitComponent } from './components/users/assign-forest-unit/assign-forest-unit.component';
import { ConfirmationComponent } from './components/users/confirmation/confirmation.component';
import { ProfileComponent } from './components/profile/profile/profile.component';
import { ChangePasswordComponent } from './components/profile/change-password/change-password.component';

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
import { MatSelectModule } from '@angular/material/select';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatMomentDateModule } from '@angular/material-moment-adapter';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

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
  MatCheckboxModule,
  MatNativeDateModule,
  MatDatepickerModule,
  MatMomentDateModule,
  MatSnackBarModule,
  MatTooltipModule,
  MatSlideToggleModule
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    FooterComponent,
    NavComponent,
    PlansTableComponent,
    PlansPageComponent,
    SubareasTableComponent,
    SubareasPageComponent,
    DivisionsTableComponent,
    DivisionsPageComponent,
    ForestUnitsTableComponent,
    ForestUnitsPageComponent,
    PlanItemsTableComponent,
    PlanItemsPageComponent,
    PlanExecutionsTableComponent,
    CreateForestUnitComponent,
    CreateDivisionComponent,
    CreateSubareaComponent,
    CreatePlanComponent,
    CreatePlanItemComponent,
    CreatePlanExecutionComponent,
    DashboardComponent,
    MiniCardComponent,
    AnnualResultsChartComponent,
    EnumToArrayPipe,
    ForestUnitFilterComponent,
    DivisionFilterComponent,
    SubareaFilterComponent,
    PlanFilterComponent,
    YearDateRangeFilterComponent,
    UsersTableComponent,
    UsersPageComponent,
    CreateUserComponent,
    AssignForestUnitComponent,
    ConfirmationComponent,
    ProfileComponent,
    ChangePasswordComponent,
  ],
  imports: [
    material,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgChartsModule,
    FormsModule
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
