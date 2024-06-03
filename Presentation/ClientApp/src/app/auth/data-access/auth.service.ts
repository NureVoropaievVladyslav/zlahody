import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/data-access/http.service';
import { AuthResponse } from '../models/AuthResponse';
import { AngularFireAuth } from '@angular/fire/compat/auth';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private afAuth: AngularFireAuth) { }

  login() {
    this.afAuth.signInWithEmailAndPassword('nicki@gmail.com', 'Testing123!')
        .then((userCredential) => {
            userCredential.user?.getIdToken().then((token) => {
                localStorage.setItem('token', token);
                localStorage.setItem('email', userCredential.user!.email!);
            });
        });
  }
}
