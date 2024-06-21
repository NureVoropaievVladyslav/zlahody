import { Component } from '@angular/core';
import { RequestService } from '../../data-access/request.service';
import { Request, RequestType } from '../../models/request';

@Component({
  selector: 'app-request-list',
  templateUrl: './request-list.component.html',
  styleUrls: ['./request-list.component.sass']
})
export class RequestListComponent {
  requests: Request[] = [];

  selectedAction = 'Available';

  constructor(private requestService: RequestService) {  }

  ngOnInit() {
    this.requestService.getAvailableRequests().subscribe(requests => {
      this.requests = requests;
    });
  }

  selectAction(action: string) {
    this.selectedAction = action;
    switch (action) {
      case 'Available':
        this.requestService.getAvailableRequests().subscribe(requests => {
          this.requests = requests;
        });
        break;
      case 'Organization':
        this.requestService.getOrganisationRequests().subscribe(requests => {
          this.requests = requests;
        });
        break;
      case 'Assigned':
        this.requestService.getAssignedRequests().subscribe(requests => {
          this.requests = requests;
        });
        break;
    }
  }

  acceptRequest(requestId: string) {
    this.requestService.acceptRequest(requestId).subscribe(() => {
      this.requests = this.requests.filter(request => request.id !== requestId);
    });
  }

  protected readonly RequestType = RequestType;
}