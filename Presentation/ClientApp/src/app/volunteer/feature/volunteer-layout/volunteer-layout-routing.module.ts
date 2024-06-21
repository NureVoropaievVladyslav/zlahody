import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VolunteerLayoutComponent } from './volunteer-layout.component';
import { ResourceListComponent } from '../resource-list/resource-list.component';

const routes: Routes = [
  {
    path: '',
    component: VolunteerLayoutComponent,
    children: [
      {
        path: 'resources',
        loadChildren: () =>
            import('../resource-list/resource-list.module').then(
                (m) => m.ResourceListModule
            ),
      },
      {
        path: '',
        redirectTo: "resources",
        pathMatch: "full"
      }
    ]
  },
  {
    path: '',
    component: VolunteerLayoutComponent,
    children: [
      {
        path: 'requests',
        loadChildren: () =>
            import('../request-list/request-list.module').then(
                (m) => m.RequestListModule
            ),
      },
    ]
  },
  {
    path: '',
    component: VolunteerLayoutComponent,
    children: [
      {
        path: 'members',
        loadChildren: () =>
            import('../member-list/member-list.module').then(
                (m) => m.MemberListModule
            ),
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes),],
  exports: [RouterModule]
})
export class VolunteerLayoutRoutingModule { }
