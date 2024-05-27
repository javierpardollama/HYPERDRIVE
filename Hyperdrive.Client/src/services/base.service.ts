import { ViewException } from './../viewmodels/views/viewexception';

import { HttpClient, HttpErrorResponse } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import {
  Observable,
  of
} from 'rxjs';

import { TimeAppVariants } from './../variants/time.app.variants';

import { TextAppVariants } from './../variants/text.app.variants';

import { CodeAppVariants } from './../variants/codes.app.variants';

import { Router } from '@angular/router';


export class BaseService {

  public constructor(
      protected httpClient: HttpClient,
      protected matSnackBar: MatSnackBar,
      protected router: Router) {
  }

  public HandleError<T>(operation = 'Operation', result?: T) {
      return (response: HttpErrorResponse): Observable<T> => {

          switch (response.status) {
              case CodeAppVariants.INTERNAL_SERVER_ERROR:
                  const exception: ViewException = {
                      Message: response.error.Message,
                      StatusCode: response.error.StatusCode
                  };

                  this.matSnackBar.open(
                      exception.Message,
                      TextAppVariants.AppOkButtonText,
                      { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
                  break;
              case CodeAppVariants.UNAUTHORIZED:
                  this.router.navigate(["unauthorized"]);
                  break;
              default:
                  this.router.navigate(["unknown"]);
                  break;
          }
          // Let the app keep running by returning an empty result.
          return of(result as T);
      };
  }
}

