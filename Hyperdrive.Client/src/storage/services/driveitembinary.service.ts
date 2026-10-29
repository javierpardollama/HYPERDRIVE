import { ViewDriveItemBinary } from '../viewmodels/views/viewdriveitembinary';

import { Service } from '@angular/core';

import { firstValueFrom } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { BaseService } from '../../shared/services/base.service';

import { environment } from '../../environments/environment';


@Service()
export class DriveItemBinaryService extends BaseService {

    public constructor() {
        super();
    }

    public FindLatestDriveItemBinaryByDriveItemId(driveitemid: string): Promise<ViewDriveItemBinary> {
        return firstValueFrom(this.httpClient.get<ViewDriveItemBinary>(`${environment.Api.Service}api/storage/v1/driveitem/binary/down/last/${driveitemid}`)
            .pipe(catchError(this.HandleError<ViewDriveItemBinary>('FindLatestDriveItemBinaryByDriveItemId', undefined))));
    }

    public FindDriveItemBinaryById(id: string): Promise<ViewDriveItemBinary> {
        return firstValueFrom(this.httpClient.get<ViewDriveItemBinary>(`${environment.Api.Service}api/storage/v1/driveitem/binary/down/${id}`)
            .pipe(catchError(this.HandleError<ViewDriveItemBinary>('FindDriveItemBinaryById', undefined))));
    }
}
