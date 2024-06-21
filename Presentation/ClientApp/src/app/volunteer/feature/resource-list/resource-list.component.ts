import { Component } from '@angular/core';
import { ResourceService } from '../../data-access/resource.service';
import { OrganizationsService } from '../../../organizations/data-access/organizations.service';
import { Resource } from '../../models/resource';
import { NgFor } from '@angular/common';
import { ResourceDialogComponent } from './resource-dialog/resource-dialog.component';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';

function mapResourceTypeToString(type: number): string | undefined {
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
    private dialogRef: MatDialog,
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

  openDialog(resourceId: string, actionName: string) {
    this.dialogRef.open(ResourceDialogComponent, {
      data: { resourceId, actionName }
    });

    this.dialogRef.afterAllClosed.subscribe(() => {
      this.resourceService.getOrganizationResources(this.organizationId).subscribe(
        (res: Resource[]) => {
          this.resources = res;
        }
      );
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
