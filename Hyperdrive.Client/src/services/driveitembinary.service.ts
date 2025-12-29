import { ViewDriveItemBinary } from '../viewmodels/views/viewdriveitembinary';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable, inject } from '@angular/core';

import { firstValueFrom } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { environment } from '../environments/environment';

import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})

export class DriveItemBinaryService extends BaseService {
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

    public FindLatestDriveItemBinaryByDriveItemId(driveitemid: number): Promise<ViewDriveItemBinary> {
        return firstValueFrom(this.httpClient.get<ViewDriveItemBinary>(`${environment.Api.Service}api/v1/driveitem/binary/down/last/${driveitemid}`)
            .pipe(catchError(this.HandleError<ViewDriveItemBinary>('FindLatestDriveItemBinaryByDriveItemId', undefined))));
    }

    public FindDriveItemBinaryById(id: number): Promise<ViewDriveItemBinary> {
        return firstValueFrom(this.httpClient.get<ViewDriveItemBinary>(`${environment.Api.Service}api/v1/driveitem/binary/down/${id}`)
            .pipe(catchError(this.HandleError<ViewDriveItemBinary>('FindDriveItemBinaryById', undefined))));
    }
}