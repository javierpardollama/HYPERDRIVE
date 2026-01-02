import { ViewDriveItemBinary } from '../viewmodels/views/viewdriveitembinary';

import { Injectable } from '@angular/core';

import { firstValueFrom } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { environment } from '../environments/environment';


@Injectable({
    providedIn: 'root',
})

export class DriveItemBinaryService extends BaseService {

    public constructor() {
        super();
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