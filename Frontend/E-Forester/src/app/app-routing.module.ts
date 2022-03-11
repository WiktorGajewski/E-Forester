import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DivisionListComponent } from './components/divisions/division-list/division-list.component';
import { ForestUnitListComponent } from './components/forest-units/forest-unit-list/forest-unit-list.component';
import { LoginComponent } from './components/login/login.component';
import { PlanItemsPageComponent } from './components/plan-items/plan-items-page/plan-items-page.component';
import { PlanListComponent } from './components/plans/plan-list/plan-list.component';
import { ProfileComponent } from './components/profile/profile/profile.component';
import { SubareaListComponent } from './components/subareas/subarea-list/subarea-list.component';
import { UserListComponent } from './components/users/user-list/user-list.component';
import { AuthGuard } from './helpers/auth.guard';
import { Role } from './models/user.model';

const routes: Routes = [
  { 
    path: "login", component: LoginComponent
  },
  { 
    path: "plans", component: PlanListComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: "subareas", component: SubareaListComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: "divisions", component: DivisionListComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: "forest-units", component: ForestUnitListComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: "plan-items", component: PlanItemsPageComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: "dashboard", component: DashboardComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: "users", component: UserListComponent,
    canActivate: [AuthGuard], data: { roles: [Role.Administrator]}
  },
  { 
    path: "profile", component: ProfileComponent,
    canActivate: [AuthGuard]
  },
  {
    path: "**", redirectTo: ""
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
