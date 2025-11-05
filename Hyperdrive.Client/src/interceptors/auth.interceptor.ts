import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';

import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { ViewApplicationUser } from '../viewmodels/views/viewapplicationuser';
import { Decrypt } from 'src/services/crypto.sevice';

@Injectable({
  providedIn: 'root'
})

export class AuthInterceptor implements HttpInterceptor {

  private User?: ViewApplicationUser;

  constructor() { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    this.GetLocalUser();

    if (this.User) {
      req = req.clone({
        setHeaders: {
          'Content-Type': 'application/json; charset=utf-8',
          'Accept': 'application/json',
          'Authorization': `Bearer ${this.User?.Token?.Value}`,
        },
      });
    }

    return next.handle(req);
  }

  // Get User from Storage
  public async GetLocalUser(): Promise<void> {
    this.User = await Decrypt(sessionStorage.getItem('User')!) as ViewApplicationUser;
  }
}
