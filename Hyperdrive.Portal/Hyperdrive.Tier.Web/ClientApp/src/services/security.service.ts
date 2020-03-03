import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';

import { SecurityPasswordChange } from './../viewmodels/security/securitypasswordchange';

import { SecurityPasswordReset } from './../viewmodels/security/securitypasswordreset';

import { SecurityEmailChange } from './../viewmodels/security/securityemailchange';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})

export class SecurityService extends BaseService {

  public constructor(
    protected httpClient: HttpClient,
    protected matSnackBar: MatSnackBar) {
    super(httpClient, matSnackBar);
  }

  public ResetPassword(viewModel: SecurityPasswordReset): Observable<ViewApplicationUser> {
    return this.httpClient.put<ViewApplicationUser>('api/applicationuser/changepassword', viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ResetPassword', undefined)));
  }

  public ChangePassword(viewModel: SecurityPasswordChange): Observable<ViewApplicationUser> {
    return this.httpClient.put<ViewApplicationUser>('api/applicationuser/changepassword', viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangePassword', undefined)));
  }

  public ChangeEmail(viewModel: SecurityEmailChange): Observable<ViewApplicationUser> {
    return this.httpClient.put<ViewApplicationUser>('api/applicationuser/changeemail', viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('ChangeEmail', undefined)));
  }
}
