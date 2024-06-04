import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'login',
    loadChildren: () =>
      import('../auth-login/auth-login.module').then(
          (m) => m.AuthLoginModule
      ),
  },
  {
    path: 'register',
    loadChildren: () =>
      import('../auth-register/auth-register.module').then(
        (m) => m.AuthRegisterModule
      ),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthShellRoutingModule { }
