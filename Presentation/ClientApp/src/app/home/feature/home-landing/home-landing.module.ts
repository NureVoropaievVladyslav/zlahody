import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeLandingRoutingModule } from './home-landing-routing.module';
import { HomeLandingComponent } from './home-landing.component';


@NgModule({
  declarations: [
    HomeLandingComponent
  ],
  imports: [
    CommonModule,
    HomeLandingRoutingModule
  ]
})
export class HomeLandingModule { }
