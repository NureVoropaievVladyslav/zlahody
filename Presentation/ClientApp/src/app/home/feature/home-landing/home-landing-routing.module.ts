import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeLandingComponent } from './home-landing.component';

const routes: Routes = [
  {
    path: '',
    component: HomeLandingComponent,
  },
  {
    path: '*',
    redirectTo: ''
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HomeLandingRoutingModule { }
