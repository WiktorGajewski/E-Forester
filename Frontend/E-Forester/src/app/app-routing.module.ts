import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PlanListComponent } from './plans/plan-list/plan-list.component';

const routes: Routes = [
  { path: "login", component: LoginComponent },
  { path: "plans", component: PlanListComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
