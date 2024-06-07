import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrganizationListRoutingModule } from './organization-list-routing.module';
import { OrganizationListComponent } from './organization-list.component';
import { OrganizationNavbarModule } from '../../ui/organization-navbar/organization-navbar.module';


@NgModule({
  declarations: [
    OrganizationListComponent
  ],
  imports: [
    CommonModule,
    OrganizationListRoutingModule,
    OrganizationNavbarModule
  ]
})
export class OrganizationListModule { }
