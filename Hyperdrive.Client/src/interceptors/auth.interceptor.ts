import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';

import { Injectable, inject } from '@angular/core';

import { from, Observable, switchMap } from 'rxjs';

import { ViewApplicationUser } from '../viewmodels/views/viewapplicationuser';
import { IsEmpty } from 'src/utils/object.utils';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';

@Injectable({
  providedIn: 'root'
})

export class AuthInterceptor implements HttpInterceptor {
  // DI
  private secureStorageService = inject(SecureStorageService);

  private User?: ViewApplicationUser;

  constructor() { }

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
    this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);
  }
}
