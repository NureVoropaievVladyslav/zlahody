import { Component } from '@angular/core';
import { ApplicationsService } from '../../data-access/applications.service';
import { Application } from '../../models/application';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.sass',
  providers: [DatePipe]
})
export class MemberListComponent {

    applications: Application[] = [];

    constructor(private applicationService: ApplicationsService) { }

    ngOnInit() {
        this.getApplications();
    }

    acceptApplication(application: Application) {
        this.applicationService.acceptApplication(application.volunteerId).subscribe({
            next: () => {
                this.getApplications();
            }
        });
    }

    kick(application: Application) {
        this.applicationService.kick(application.volunteerId).subscribe({
            next: () => {
                this.getApplications();
            }
        });
    }

    private getApplications() {
        this.applicationService.getApplications().subscribe({
            next: (applications) => {
                this.applications = applications;
            }
        });
    }
}
