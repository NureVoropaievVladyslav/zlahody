import { Component } from '@angular/core';
import { OrganizationsService } from '../../data-access/organizations.service';
import { Organization } from '../../models/organization';
import { activate } from '@angular/fire/remote-config';

@Component({
  selector: 'app-organization-list',
  templateUrl: './organization-list.component.html',
  styleUrl: './organization-list.component.sass'
})
export class OrganizationListComponent {

  organizations: Organization[] = []

  constructor(private organizationsService: OrganizationsService) { }

  ngOnInit() {
    this.organizationsService.get()
        .subscribe(organizations => this.organizations = organizations);
  }

  join(organizationId: string) {
    this.organizationsService.join(organizationId).subscribe();
  }
}
