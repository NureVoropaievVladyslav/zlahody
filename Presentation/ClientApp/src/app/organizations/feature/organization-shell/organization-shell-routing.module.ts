import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    loadChildren: () =>
        import('../organization-list/organization-list.module').then(
            (m) => m.OrganizationListModule
        ),
  },
  {
    path: 'applications',
    loadChildren: () =>
        import('../organization-applications/organization-applications.module').then(
            (m) => m.OrganizationApplicationsModule
        ),
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrganizationShellRoutingModule { }
