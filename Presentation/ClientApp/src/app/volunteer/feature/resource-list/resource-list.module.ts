import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ResourceListRoutingModule } from './resource-list-routing.module';
import { ResourceListComponent } from './resource-list.component';
import { ResourceDialogComponent } from './resource-dialog/resource-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    ResourceListComponent,
    ResourceDialogComponent
  ],
  imports: [
    CommonModule,
    ResourceListRoutingModule,
    ReactiveFormsModule,
  ]
})
export class ResourceListModule { }
