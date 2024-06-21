import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RequestListRoutingModule } from './request-list-routing.module';
import { RequestListComponent } from './request-list.component';


@NgModule({
  declarations: [
    RequestListComponent
  ],
  imports: [
    CommonModule,
    RequestListRoutingModule
  ]
})
export class RequestListModule { }
