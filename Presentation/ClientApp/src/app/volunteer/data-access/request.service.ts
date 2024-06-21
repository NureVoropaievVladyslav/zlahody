import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/data-access/http.service';
import { Request } from '../models/request';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  constructor(private http: HttpService) { }

  getAvailableRequests() {
    return this.http.get<Request[]>('/requests');
  }

  getOrganisationRequests() {
    return this.http.get<Request[]>(`/requests/organisation`);
  }

  getAssignedRequests() {
    return this.http.get<Request[]>('/requests/assigned');
  }

  acceptRequest(requestId: string) {
    return this.http.post(`/requests/${requestId}/assign`, {});
  }
}
