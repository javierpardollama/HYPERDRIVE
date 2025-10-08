import { ViewApplicationUser } from '../viewmodels/views/viewapplicationuser';

import { SecurityPasswordChange } from '../viewmodels/security/securitypasswordchange';

import { SecurityPasswordReset } from '../viewmodels/security/securitypasswordreset';

import { SecurityEmailChange } from '../viewmodels/security/securityemailchange';

import { SecurityPhoneNumberChange } from '../viewmodels/security/securityphonenumberchange';

import { SecurityNameChange } from '../viewmodels/security/securitynamechange';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

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

  public constructor(
    protected override httpClient: HttpClient,
    protected override matSnackBar: MatSnackBar,
    protected override router: Router) {
    super(httpClient, matSnackBar, router);
  }

  public ResetPassword(viewModel: SecurityPasswordReset): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/resetpassword`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ResetPassword', undefined))));
  }

  public ChangePassword(viewModel: SecurityPasswordChange): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/changepassword`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangePassword', undefined))));
  }

  public ChangeEmail(viewModel: SecurityEmailChange): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/changeemail`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangeEmail', undefined))));
  }

  public ChangePhoneNumber(viewModel: SecurityPhoneNumberChange): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/changephonenumber`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangePhoneNumber', undefined))));
  }

  public ChangeName(viewModel: SecurityNameChange): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/changename`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangeName', undefined))));
  }

  public RefreshTokens(viewModel: SecurityRefreshTokenReset): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/security/refreshtokens`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('RefreshTokens', undefined))));
  }
}
