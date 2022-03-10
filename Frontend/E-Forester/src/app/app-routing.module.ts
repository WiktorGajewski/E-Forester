import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DivisionListComponent } from './components/divisions/division-list/division-list.component';
import { ForestUnitListComponent } from './components/forest-units/forest-unit-list/forest-unit-list.component';
import { LoginComponent } from './components/login/login.component';
import { PlanItemsPageComponent } from './components/plan-items/plan-items-page/plan-items-page.component';
import { PlanListComponent } from './components/plans/plan-list/plan-list.component';
import { SubareaListComponent } from './components/subareas/subarea-list/subarea-list.component';
import { UserListComponent } from './components/users/user-list/user-list.component';

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "plans", component: PlanListComponent },
  { path: "subareas", component: SubareaListComponent },
  { path: "divisions", component: DivisionListComponent },
  { path: "forest-units", component: ForestUnitListComponent },
  { path: "plan-items", component: PlanItemsPageComponent },
  { path: "dashboard", component: DashboardComponent },
  { path: "users", component: UserListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
