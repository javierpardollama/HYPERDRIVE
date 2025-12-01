import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';

import { Injectable } from '@angular/core';

import { from, Observable, switchMap } from 'rxjs';

import { ViewApplicationUser } from '../viewmodels/views/viewapplicationuser';
import { IsEmpty } from 'src/utils/object.utils';
import { SessionService } from 'src/services/session.service';

@Injectable({
  providedIn: 'root'
})

export class AuthInterceptor implements HttpInterceptor {

  private User?: ViewApplicationUser;

  constructor(private sessionService: SessionService) { }


  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return from(this.GetLocalUser()).pipe(
      switchMap(() => {
        if (!IsEmpty(this.User)) {
          req = req.clone({
            setHeaders: {
              'Content-Type': 'application/json; charset=utf-8',
              'Accept': 'application/json',
              'Authorization': `Bearer ${this.User?.Token?.Value}`,
            },
          });
        }
        return next.handle(req); // return original Observable
      })
    );
  }


  // Get User from Storage
  public async GetLocalUser(): Promise<void> {
    this.User = await this.sessionService.DecryptUser();
  }
}
