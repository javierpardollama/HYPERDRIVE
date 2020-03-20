import { AuthJoinIn } from './../viewmodels/auth/authjoinin';

import { AuthSignIn } from './../viewmodels/auth/authsignin';

import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})

export class AuthService extends BaseService {

  public constructor(
    protected httpClient: HttpClient,
    protected matSnackBar: MatSnackBar) {
    super(httpClient, matSnackBar);
  }

  public SignIn(viewModel: AuthSignIn) : Promise<ViewApplicationUser> {

    return this.httpClient.post<ViewApplicationUser>('api/auth/signin', viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('SignIn', undefined))).toPromise();
  }

  public JoinIn(viewModel: AuthJoinIn) : Promise<ViewApplicationUser> {
    return this.httpClient.post<ViewApplicationUser>('api/auth/joinin', viewModel)
      .pipe(catchError(this.HandleError<ViewApplicationUser>('JoinIn', undefined))).toPromise();
  }
}
