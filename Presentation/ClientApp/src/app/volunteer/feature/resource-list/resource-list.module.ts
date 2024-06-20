import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ResourceListRoutingModule } from './resource-list-routing.module';
import { ResourceListComponent } from './resource-list.component';


@NgModule({
  declarations: [
    ResourceListComponent
  ],
  imports: [
    CommonModule,
    ResourceListRoutingModule
  ]
})
export class ResourceListModule { }
