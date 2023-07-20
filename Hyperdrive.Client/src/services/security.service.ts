import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';

import { SecurityPasswordChange } from './../viewmodels/security/securitypasswordchange';

import { SecurityPasswordReset } from './../viewmodels/security/securitypasswordreset';

import { SecurityEmailChange } from './../viewmodels/security/securityemailchange';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { firstValueFrom } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { environment } from './../environments/environment';

@Injectable({
  providedIn: 'root',
})

export class SecurityService extends BaseService {

  public constructor(
    protected override httpClient: HttpClient,
    protected override matSnackBar: MatSnackBar) {
    super(httpClient, matSnackBar);
  }

  public ResetPassword(viewModel: SecurityPasswordReset): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/security/changepassword`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ResetPassword', undefined))));
  }

  public ChangePassword(viewModel: SecurityPasswordChange): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/security/changepassword`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangePassword', undefined))));
  }

  public ChangeEmail(viewModel: SecurityEmailChange): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/security/changeemail`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangeEmail', undefined))));
  }
}
