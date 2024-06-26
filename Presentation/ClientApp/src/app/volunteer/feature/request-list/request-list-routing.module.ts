import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RequestListComponent } from './request-list.component';

const routes: Routes = [
  {
    path: '',
    component: RequestListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RequestListRoutingModule { }
