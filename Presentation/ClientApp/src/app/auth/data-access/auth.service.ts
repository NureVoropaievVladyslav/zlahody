import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/data-access/http.service';
import { AuthResponse } from '../models/AuthResponse';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { Observable, from } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private afAuth: AngularFireAuth) { }

  login(email: string, password: string): Observable<void> {
    return from(
      this.afAuth.signInWithEmailAndPassword(email, password)
        .then((userCredential) => {
          return userCredential.user?.getIdToken().then((token) => {
            localStorage.setItem('token', token);
            localStorage.setItem('email', userCredential.user!.email!);
          });
      })
    );
  }

}
