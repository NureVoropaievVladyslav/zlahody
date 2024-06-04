import { Component } from '@angular/core';
import { AuthService } from '../../data-access/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-auth-register',
  templateUrl: './auth-register.component.html',
  styleUrl: './auth-register.component.sass'
})
export class AuthRegisterComponent {
  registerForm: FormGroup = this.fb.group({
    fullName: ['', [Validators.required]],
    email: ['', [
      Validators.required, 
      Validators.email
    ]],
    password: ['', [
      Validators.required,
      Validators.minLength(8),
      Validators.maxLength(32),
      Validators.pattern(/[A-Z]+/),
      Validators.pattern(/[a-z]+/), 
      Validators.pattern(/[0-9]+/),
      Validators.pattern(/[\!\?\*\.]+/) 
    ]],
  })

  constructor(
    private authService: AuthService,
    private fb: FormBuilder, 
    private toastr: ToastrService,
  ) { }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get fullName() {
    return this.registerForm.get('fullName');
  }

  onSubmit(){
    if(this.registerForm.valid){
      this.authService.register(this.fullName?.value, this.email?.value, this.password?.value);
    }
  }

}
