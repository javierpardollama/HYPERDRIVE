import { ViewDriveItemVersion } from '../viewmodels/views/viewdriveitemversion';

import { ViewPage } from '../viewmodels/views/viewpage';

import { FilterPageDriveItemVersion } from '..//viewmodels/filters/filterpagedriveitemversion';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { firstValueFrom } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { environment } from '../environments/environment';

import { Router } from '@angular/router';



@Injectable({
    providedIn: 'root',
})

export class DriveItemVersionService extends BaseService {

    public constructor(
        protected override httpClient: HttpClient,
        protected override matSnackBar: MatSnackBar,
        protected override router: Router) {
        super(httpClient, matSnackBar, router);
    }

    public FindPaginatedDriveItemVersionByDriveItemId(page: FilterPageDriveItemVersion): Promise<ViewPage<ViewDriveItemVersion>> {
        return firstValueFrom(this.httpClient.post<ViewPage<ViewDriveItemVersion>>(`${environment.Api.Service}api/v1/driveitem/version/page`, page)
            .pipe(catchError(this.HandleError<ViewPage<ViewDriveItemVersion>>('FindPaginatedDriveItemVersionByDriveItemId', undefined))));
    }

     public TargetDriveItemVersionById(id: number): Promise<void> {
        return firstValueFrom(this.httpClient.post<any>(`${environment.Api.Service}api/v1/driveitem/version/target/${id}`)
            .pipe(catchError(this.HandleError<any>('TargetDriveItemVersionById', undefined))));
    }
}
