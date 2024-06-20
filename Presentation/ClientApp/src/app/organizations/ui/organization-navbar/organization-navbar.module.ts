import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrganizationNavbarComponent } from './organization-navbar.component';
import { RouterLink, RouterLinkActive } from '@angular/router';



@NgModule({
    declarations: [
        OrganizationNavbarComponent
    ],
    exports: [
        OrganizationNavbarComponent
    ],
    imports: [
        CommonModule,
        RouterLink,
        RouterLinkActive
    ]
})
export class OrganizationNavbarModule { }
