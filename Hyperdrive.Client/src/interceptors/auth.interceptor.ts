import { HttpInterceptorFn } from '@angular/common/http';

import { inject } from '@angular/core';

import { from, switchMap } from 'rxjs';

import { ViewApplicationUser } from '../viewmodels/views/viewapplicationuser';
import { IsEmpty } from 'src/utils/object.utils';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';


export const AuthInterceptor: HttpInterceptorFn = (req, next) => {
  const secureStorageService = inject(SecureStorageService);

  return from(
    secureStorageService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY)
  ).pipe(
    switchMap((user) => {
      if (!IsEmpty(user)) {
        req = req.clone({
          setHeaders: {
            'Content-Type': 'application/json; charset=utf-8',
            'Accept': 'application/json',
            'Authorization': `Bearer ${user?.Token?.Value}`,
          },
        });
      }
      return next(req); // return original Observable
    })
  );
};