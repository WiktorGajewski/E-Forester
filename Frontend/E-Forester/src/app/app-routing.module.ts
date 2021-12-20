import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { PlanListComponent } from './components/plans/plan-list/plan-list.component';
import { SubareaListComponent } from './components/subareas/subarea-list/subarea-list.component';

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "plans", component: PlanListComponent },
  { path: "subareas", component: SubareaListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
