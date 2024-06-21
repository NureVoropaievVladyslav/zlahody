import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ResourceService } from '../../../data-access/resource.service';
import { Resource } from '../../../models/resource';

@Component({
  selector: 'app-resource-dialog',
  templateUrl: './resource-dialog.component.html',
  styleUrl: './resource-dialog.component.sass'
})
export class ResourceDialogComponent {

  types: string[] = [ 'Shelter', 'Medical', 'Food', 'TemporaryHousing', 'Other'];

  createForm: FormGroup = this.fb.group({
    title: ['', Validators.required],
    description: ['', Validators.required],
    type: ['', Validators.required],
    address: [''],
    phone: ['']
  });

  constructor(
    private fb: FormBuilder,
    private resourceService: ResourceService,
    private dialogRef: MatDialogRef<ResourceDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { resourceId: string, actionName: string }
  ) {}

  typeMapping: { [key: string]: number } = {
    'Shelter': 0,
    'Medical': 1,
    'Food': 2,
    'TemporaryHousing': 3,
    'Other': 4
  };

  ngOnInit(): void {
    // switch (this.data.actionName) {
    //   case 'edit':
    //     this.resourceService.getById(this.data.resourceId).subscribe(
    //       (resource: Resource) => {
    //         this.createForm.patchValue({
    //           title: resource.title,
    //           description: resource.description,
    //           type: resource.type,
    //           adress: resource.adress,
    //           phone: resource.phone
    //         });
    //       }
    //     );
    //     break;
    //   default:
    //     break;
    // }
  }

  get title() {
    return this.createForm.get('title');
  }

  get description() {
    return this.createForm.get('description');
  }

  get type() {
    return this.createForm.get('type');
  }

  get address() {
    return this.createForm.get('address');
  }
  
  get phone() {
    return this.createForm.get('phone');
  }

  onSubmit() {
    if (this.createForm.valid) {
      const resource: Resource = {
        id: this.data.resourceId,
        title: this.createForm.value.title,
        description: this.createForm.value.description,
        type: this.typeMapping[this.createForm.value.type],
        address: this.createForm.value.address,
        phone: this.createForm.value.phone,
      };

      switch (this.data.actionName) {
        case 'create':
          this.resourceService.createResource(resource).subscribe(() => {
            this.dialogRef.close();
          });
          break;
        // case 'update':
        //   this.resourceService.updateResource(resource).subscribe(() => {
        //     this.dialogRef.close();
        //   });
        //   break;
        default:
          this.dialogRef.close();
          break;
      }
    }
  }

}
