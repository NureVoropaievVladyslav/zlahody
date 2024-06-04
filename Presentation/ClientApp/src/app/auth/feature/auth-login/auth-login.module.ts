import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuthLoginRoutingModule } from './auth-login-routing.module';
import { AuthLoginComponent } from './auth-login.component';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
  declarations: [
    AuthLoginComponent
  ],
  imports: [
    CommonModule,
    AuthLoginRoutingModule,
    ReactiveFormsModule,
  ]
})
export class AuthLoginModule { }
