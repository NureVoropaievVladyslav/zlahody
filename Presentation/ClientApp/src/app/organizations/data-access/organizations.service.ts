import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/data-access/http.service';
import { Application, Organization, OrganizationApplication } from '../models/organization';

@Injectable({
  providedIn: 'root'
})
export class OrganizationsService {

  constructor(private http: HttpService) { }

  get() {
    return this.http.get<Organization[]>('/organizations');
  }

  getApplications() {
    return this.http.get<Application[]>('/organizations/applications');
  }

  getPersonal() {
    return this.http.get<string>('/organizations/personal');
  }

  join(organizationId: string) {
    return this.http.post(`/organizations/${organizationId}/users`, null);
  }

  getOrganizationApplications(organizationId: string) {
    return this.http.get<OrganizationApplication[]>(`/organizations/${organizationId}/applications`);
  }

  accept(organizationId: string, volunteerId: string) {
    return this.http.post(`/organizations/${organizationId}/users/${volunteerId}`, null);
  }

  kick(organizationId: string, volunteerId: string) {
    return this.http.put(`/organizations/${organizationId}/users/${volunteerId}`, null);
  }
}
