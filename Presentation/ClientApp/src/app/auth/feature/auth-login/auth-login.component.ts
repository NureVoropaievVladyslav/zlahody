import { Component } from '@angular/core';
import { AuthService } from '../../data-access/auth.service';

@Component({
  selector: 'app-auth-login',
  templateUrl: './auth-login.component.html',
  styleUrl: './auth-login.component.sass'
})
export class AuthLoginComponent {

  constructor(private authService: AuthService) { }

  login() {
    this.authService.login();
  }
}
