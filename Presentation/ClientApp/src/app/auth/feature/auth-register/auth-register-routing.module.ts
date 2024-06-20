import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthRegisterComponent } from './auth-register.component';
import { FormGroup } from '@angular/forms';

const routes: Routes = [
  {
    path: '',
    component: AuthRegisterComponent
  }
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRegisterRoutingModule { }
