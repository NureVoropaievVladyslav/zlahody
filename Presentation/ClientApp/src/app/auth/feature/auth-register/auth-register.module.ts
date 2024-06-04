import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthRegisterRoutingModule } from './auth-register-routing.module';
import { AuthRegisterComponent } from './auth-register.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AuthRegisterComponent
  ],
  imports: [
    CommonModule,
    AuthRegisterRoutingModule,
    ReactiveFormsModule,
  ]
})
export class AuthRegisterModule { }
