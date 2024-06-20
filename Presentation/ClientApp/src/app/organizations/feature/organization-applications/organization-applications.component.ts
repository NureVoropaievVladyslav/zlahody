import { Component } from '@angular/core';
import { Application, Organization, OrganizationApplication } from '../../models/organization';
import { OrganizationsService } from '../../data-access/organizations.service';

@Component({
  selector: 'app-organization-applications',
  templateUrl: './organization-applications.component.html',
  styleUrl: './organization-applications.component.sass'
})
export class OrganizationApplicationsComponent {

  applications: Application[] = []
  organizationApplications: OrganizationApplication[] = []
  private organizationId: string = '';

  constructor(private organizationsService: OrganizationsService) { }

  ngOnInit() {
    this.organizationsService.getPersonal().subscribe(organizationId => {
      if (organizationId === '00000000-0000-0000-0000-000000000000') {
        this.organizationsService.getApplications().subscribe(applications => this.applications = applications);
      } else {
        this.organizationsService.getOrganizationApplications(organizationId).subscribe(applications => this.organizationApplications = applications);
        this.organizationId = organizationId;
      }
    });
  }

  accept(application: OrganizationApplication) {
    this.organizationsService.accept(this.organizationId, application.volunteerId).subscribe(() => application.isAccepted = true);
  }

  kick(application: OrganizationApplication) {
    this.organizationsService.kick(this.organizationId, application.volunteerId).subscribe(() => this.organizationApplications = this.organizationApplications.filter(a => a.volunteerId !== application.volunteerId));
  }
}
