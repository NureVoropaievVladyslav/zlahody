import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrganizationApplicationsComponent } from './organization-applications.component';

const routes: Routes = [
  {
    path: '',
    component: OrganizationApplicationsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrganizationApplicationsRoutingModule { }
