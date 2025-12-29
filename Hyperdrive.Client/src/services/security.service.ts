import { ViewApplicationUser } from '../viewmodels/views/viewapplicationuser';

import { SecurityPasswordChange } from '../viewmodels/security/securitypasswordchange';

import { SecurityPasswordReset } from '../viewmodels/security/securitypasswordreset';

import { SecurityEmailChange } from '../viewmodels/security/securityemailchange';

import { SecurityPhoneNumberChange } from '../viewmodels/security/securityphonenumberchange';

import { SecurityNameChange } from '../viewmodels/security/securitynamechange';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable, inject } from '@angular/core';

import { firstValueFrom } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { environment } from '../environments/environment';

import { Router } from '@angular/router';
import { SecurityRefreshTokenReset } from 'src/viewmodels/security/securityrefreshtokenreset';

@Injectable({
  providedIn: 'root',
})

export class SecurityService extends BaseService {
  protected override httpClient: HttpClient;
  protected override matSnackBar: MatSnackBar;
  protected override router: Router;

  public constructor() {
    const httpClient = inject(HttpClient);
    const matSnackBar = inject(MatSnackBar);
    const router = inject(Router);

    super(httpClient, matSnackBar, router);
  
    this.httpClient = httpClient;
    this.matSnackBar = matSnackBar;
    this.router = router;
  }

  public ResetPassword(viewModel: SecurityPasswordReset): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/password/reset`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ResetPassword', undefined))));
  }

  public ChangePassword(viewModel: SecurityPasswordChange): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/password/change`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangePassword', undefined))));
  }

  public ChangeEmail(viewModel: SecurityEmailChange): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/email/change`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangeEmail', undefined))));
  }

  public ChangePhoneNumber(viewModel: SecurityPhoneNumberChange): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/phonenumber/change`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangePhoneNumber', undefined))));
  }

  public ChangeName(viewModel: SecurityNameChange): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/name/change`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangeName', undefined))));
  }

  public RefreshTokens(viewModel: SecurityRefreshTokenReset): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/refreshtokens`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('RefreshTokens', undefined))));
  }
}
