import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/data-access/http.service';
import { Application } from '../models/application';

@Injectable({
  providedIn: 'root'
})
export class ApplicationsService {

  constructor(private http: HttpService) { }

  getApplications() {
    return this.http.get<Application[]>('/organizations/personal/applications');
  }

  acceptApplication(userId: string) {
    return this.http.post(`/organizations/personal/users/${userId}`, {});
  }

  kick(userId: string) {
    return this.http.put(`/organizations/personal/users/${userId}`, {});
  }
}
