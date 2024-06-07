import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationApplicationsRoutingModule } from './organization-applications-routing.module';
import { OrganizationApplicationsComponent } from './organization-applications.component';
import { OrganizationNavbarModule } from '../../ui/organization-navbar/organization-navbar.module';


@NgModule({
  declarations: [
    OrganizationApplicationsComponent
  ],
    imports: [
        CommonModule,
        OrganizationApplicationsRoutingModule,
        OrganizationNavbarModule
    ]
})
export class OrganizationApplicationsModule { }
