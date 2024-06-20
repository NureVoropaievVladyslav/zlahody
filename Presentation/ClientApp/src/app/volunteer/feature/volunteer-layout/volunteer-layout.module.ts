import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VolunteerLayoutRoutingModule } from './volunteer-layout-routing.module';
import { VolunteerLayoutComponent } from './volunteer-layout.component';
import { VolunteerSidebarModule } from '../volunteer-sidebar/volunteer-sidebar.module';


@NgModule({
  declarations: [
    VolunteerLayoutComponent
  ],
  imports: [
    CommonModule,
    VolunteerLayoutRoutingModule,
    VolunteerSidebarModule
  ]
})
export class VolunteerLayoutModule { }
