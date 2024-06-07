import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';


@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private router: Router, 
    private toastr: ToastrService
    ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if(error){
          switch(error.status){
            case 401:
              this.toastr.error("Unauthorized", error.status.toString())
                this.router.navigate(['auth/login']);
            break;
            default:
              this.toastr.error(error.error.detail);
            break;
          }
        }
        throw error;
      })
    );
  }
}
