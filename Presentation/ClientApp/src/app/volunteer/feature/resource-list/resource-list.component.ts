import { Component } from '@angular/core';
import { ResourceService } from '../../data-access/resource.service';
import { OrganizationsService } from '../../../organizations/data-access/organizations.service';
import { Resource } from '../../models/resource';
import { NgFor } from '@angular/common';

export type ResourceType = 'Shelter' | 'Medical' | 'Food' | 'TemporaryHousing' | 'Other';

function mapResourceTypeToString(type: number): ResourceType | undefined {
  switch (type) {
    case 0:
      return 'Shelter';
    case 1:
      return 'Medical';
    case 2:
      return 'Food';
    case 3:
      return 'TemporaryHousing';
    case 4:
      return 'Other';
    default:
      return undefined;
  }
}

@Component({
  selector: 'app-resource-list',
  templateUrl: './resource-list.component.html',
  styleUrl: './resource-list.component.sass'
})
export class ResourceListComponent {
  resources: Resource[] = [];
  private organizationId: string = '';

  constructor(
    private resourceService: ResourceService,
    private organizationsService: OrganizationsService,
    ) { }

  ngOnInit(): void {
    this.organizationsService.getPersonal().subscribe(
      (organizationId: string) => {
        this.organizationId = organizationId;
        this.resourceService.getOrganizationResources(this.organizationId).subscribe(
          (res: any[]) => {
            this.resources = res.map(item => ({
              ...item,
              type: mapResourceTypeToString(item.type) || 'Other'
            }));
          });
      });
  }

  onDelete(resource: Resource){
    this.resourceService.deleteResource(resource.id).subscribe();
    const index = this.resources.indexOf(resource);
      if (index !== -1) {
        this.resources.splice(index, 1);
      }
  }
}
