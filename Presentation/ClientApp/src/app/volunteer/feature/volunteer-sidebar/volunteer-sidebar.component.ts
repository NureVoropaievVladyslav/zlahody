import { Component } from '@angular/core';
import { AuthService } from '../../../auth/data-access/auth.service';

@Component({
  selector: 'app-volunteer-sidebar',
  templateUrl: './volunteer-sidebar.component.html',
  styleUrl: './volunteer-sidebar.component.sass'
})
export class VolunteerSidebarComponent {

  constructor(private authService: AuthService) {  }

  logout() {
    this.authService.logout();
  }

  getRole(): string {
    return this.authService.getRole();
  }
}
