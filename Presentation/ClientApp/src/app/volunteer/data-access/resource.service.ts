import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/data-access/http.service';
import { Resource } from '../models/resource';

@Injectable({
  providedIn: 'root'
})
export class ResourceService {

  constructor(private http: HttpService) { }

  getOrganizationResources(organizationId: string){
    return this.http.get<Resource[]>(`/resources/organizations/${organizationId}`);
  }

  deleteResource(resourceId: string){
    return this.http.delete(`/resources`, resourceId);
  }

  createResource(resource: Resource){
    return this.http.post(`/resources`, 
      {
        title: resource.title,
        description: resource.description,
        type: resource.type,
        address: resource.address,
        phone: resource.phone
      } )
  }

  updateResource(resource: Resource){
    return this.http.put(`/resources`, 
      {
        title: resource.title,
        description: resource.description,
        type: resource.type,
        address: resource.address,
        phone: resource.phone
      } )
  }
}
