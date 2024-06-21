import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/data-access/http.service';
import { AuthResponse } from '../models/AuthResponse';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { Observable, from } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private afAuth: AngularFireAuth,
    private httpService: HttpService,
    private router: Router,
    private toastr: ToastrService,
  ) { }

  login(email: string, password: string): Observable<void> {
    return from(
      this.afAuth.signInWithEmailAndPassword(email, password)
        .then((userCredential) => {
          userCredential.user?.getIdTokenResult().then((res) => sessionStorage.setItem('role', res.claims['role']));

          return userCredential.user?.getIdToken().then((token) => {

            localStorage.setItem('token', token);
            localStorage.setItem('email', userCredential.user!.email!);
            this.router.navigate(['volunteer/']);
          });
      })
    );
  }

  register(fullName: string, email: string, password: string){
    this.httpService.post('/users', {fullName, email, password}).subscribe({
      next: () => {
        this.router.navigate(['auth/login']);
      }
    });
  }

    logout() {
        this.afAuth.signOut().then(() => {
          localStorage.removeItem('token');
          localStorage.removeItem('email');
          this.router.navigate(['auth/login']);
        });
    }

  getRole(): string {
    return sessionStorage.getItem('role')!;
  }
}
