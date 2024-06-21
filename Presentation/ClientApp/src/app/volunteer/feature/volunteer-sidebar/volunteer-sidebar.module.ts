import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VolunteerSidebarRoutingModule } from './volunteer-sidebar-routing.module';
import { VolunteerSidebarComponent } from './volunteer-sidebar.component';


@NgModule({
  declarations: [
    VolunteerSidebarComponent
  ],
  exports: [
    VolunteerSidebarComponent
],
  imports: [
    CommonModule,
    VolunteerSidebarRoutingModule
  ]
})
export class VolunteerSidebarModule { }
