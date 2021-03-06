import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DivisionsPageComponent } from './components/divisions/divisions-page/divisions-page.component';
import { ForestUnitsPageComponent } from './components/forest-units/forest-units-page/forest-units-page.component';
import { LoginComponent } from './components/login/login.component';
import { PlanItemsPageComponent } from './components/plan-items/plan-items-page/plan-items-page.component';
import { PlansPageComponent } from './components/plans/plans-page/plans-page.component';
import { ProfileComponent } from './components/profile/profile/profile.component';
import { SubareasPageComponent } from './components/subareas/subareas-page/subareas-page.component';
import { UsersPageComponent } from './components/users/users-page/users-page.component';
import { AuthGuard } from './helpers/auth.guard';
import { Role } from './models/user.model';

const routes: Routes = [
  { 
    path: "login", component: LoginComponent
  },
  { 
    path: "plans", component: PlansPageComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: "subareas", component: SubareasPageComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: "divisions", component: DivisionsPageComponent,
    canActivate: [AuthGuard]
  },
  { 
    path: "forest-units", component: ForestUnitsPageComponent,
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
    path: "users", component: UsersPageComponent,
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
