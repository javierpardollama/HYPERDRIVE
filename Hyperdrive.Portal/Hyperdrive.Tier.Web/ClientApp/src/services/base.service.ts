import { ViewException } from './../viewmodels/views/viewexception';

import {
  HttpClient,
  HttpErrorResponse
} from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import {
  Observable,
  of
} from 'rxjs';

import { TimeAppVariants } from './../variants/time.app.variants';

import { TextAppVariants } from './../variants/text.app.variants';

export class BaseService {

  public constructor(
    protected httpClient: HttpClient,
    protected matSnackBar: MatSnackBar) {

  }

  public HandleError<T>(operation = 'Operation', result?: T) {
    return (response: HttpErrorResponse): Observable<T> => {

      const expception: ViewException = {
        Message: response.error.Message,
        StatusCode: response.error.StatusCode
      };

      this.matSnackBar.open(
        expception.Message,
        TextAppVariants.AppOkButtonText,
        { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}

