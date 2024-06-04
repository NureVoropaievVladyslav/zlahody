import { Component } from '@angular/core';
import { AuthService } from '../../data-access/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-auth-login',
  templateUrl: './auth-login.component.html',
  styleUrl: './auth-login.component.sass'
})
export class AuthLoginComponent {

  loginForm: FormGroup = this.fb.group({
    email: ['', [Validators.required]],
    password: ['', Validators.required]
  });

  constructor(
    private authService: AuthService,
    private fb: FormBuilder, 
    private toastr: ToastrService
  ) { }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }

  onSubmit() {
    if(this.loginForm.valid){
      this.authService.login(this.email?.value, this.password?.value).subscribe({
        error: (error) => {
          this.toastr.error("Invalid email or password.")
        }
      });
    }
  }
}
