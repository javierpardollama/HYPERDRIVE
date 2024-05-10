import { AuthJoinIn } from './../viewmodels/auth/authjoinin';

import { AuthSignIn } from './../viewmodels/auth/authsignin';

import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { firstValueFrom } from 'rxjs';

import { environment } from './../environments/environment';

import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})

export class AuthService extends BaseService {

  public constructor(
    protected override httpClient: HttpClient,
    protected override matSnackBar: MatSnackBar,
    protected override router: Router) {
    super(httpClient, matSnackBar, router);
  }

  public SignIn(viewModel: AuthSignIn): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.post<ViewApplicationUser>(`${environment.Api.Service}api/auth/signin`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('SignIn', undefined))));
  }

  public JoinIn(viewModel: AuthJoinIn): Promise<ViewApplicationUser> {
    return firstValueFrom(this.httpClient.post<ViewApplicationUser>(`${environment.Api.Service}api/auth/joinin`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('JoinIn', undefined))));
  }

  public SignOut(viewModel: ViewApplicationUser): Promise<any> {
    return firstValueFrom(this.httpClient.post<ViewApplicationUser>(`${environment.Api.Service}api/auth/signout`, viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('SignOut', undefined))));
  }
}
