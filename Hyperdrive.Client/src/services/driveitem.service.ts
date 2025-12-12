import { AddDriveItem } from '../viewmodels/additions/adddriveitem';

import { UpdateDriveItemName } from '../viewmodels/updates/updatedriveitemname';

import { UpdateDriveItemSharedWith } from '../viewmodels/updates/updatedriveitemsharedwith';

import { ViewDriveItem } from '../viewmodels/views/viewdriveitem';

import { ViewDriveItemVersion } from '../viewmodels/views/viewdriveitemversion';

import { ViewDriveItemBinary } from '../viewmodels/views/viewdriveitembinary';

import { ViewPage } from '../viewmodels/views/viewpage';

import { FilterPageDriveItem } from '../viewmodels/filters/filterpagedriveitem';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { firstValueFrom } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { environment } from '../environments/environment';

import { Router } from '@angular/router';
import { FilterPageDriveItemVersion } from 'src/viewmodels/filters/filterpagedriveitemversion';

@Injectable({
    providedIn: 'root',
})

export class DriveItemService extends BaseService {

    public constructor(
        protected override httpClient: HttpClient,
        protected override matSnackBar: MatSnackBar,
        protected override router: Router) {
        super(httpClient, matSnackBar, router);
    }

    public UpdateDriveItemName(viewModel: UpdateDriveItemName): Promise<ViewDriveItem> {
        return firstValueFrom(this.httpClient.put<ViewDriveItem>(`${environment.Api.Service}api/v1/driveitem/name/change`, viewModel)
            .pipe(catchError(this.HandleError<ViewDriveItem>('UpdateDriveItemName', undefined))));
    }

    public UpdateDriveItemSharedWith(viewModel: UpdateDriveItemSharedWith): Promise<ViewDriveItem> {
        return firstValueFrom(this.httpClient.put<ViewDriveItem>(`${environment.Api.Service}api/v1/driveitem/share`, viewModel)
            .pipe(catchError(this.HandleError<ViewDriveItem>('UpdateDriveItemSharedWith', undefined))));
    }

    public FindPaginatedDriveItemByApplicationUserId(page: FilterPageDriveItem): Promise<ViewPage<ViewDriveItem>> {
        return firstValueFrom(this.httpClient.post<ViewPage<ViewDriveItem>>(`${environment.Api.Service}api/v1/driveitem/page`, page)
            .pipe(catchError(this.HandleError<ViewPage<ViewDriveItem>>('FindPaginatedDriveItemByApplicationUserId', undefined))));
    }

    public FindPaginatedSharedDriveItemByApplicationUserId(page: FilterPageDriveItem): Promise<ViewPage<ViewDriveItem>> {
        return firstValueFrom(this.httpClient.post<ViewPage<ViewDriveItem>>(`${environment.Api.Service}api/v1/driveitem/page/shared`, page)
            .pipe(catchError(this.HandleError<ViewPage<ViewDriveItem>>('FindPaginatedSharedDriveItemByApplicationUserId', undefined))));
    }

    public FindPaginatedDriveItemVersionByDriveItemId(page: FilterPageDriveItemVersion): Promise<ViewPage<ViewDriveItemVersion>> {
        return firstValueFrom(this.httpClient.post<ViewPage<ViewDriveItemVersion>>(`${environment.Api.Service}api/v1/driveitem/page/version`, page)
            .pipe(catchError(this.HandleError<ViewPage<ViewDriveItemVersion>>('FindPaginatedDriveItemVersionByDriveItemId', undefined))));
    }

    public FindLatestDriveItemBinaryById(driveitemid: number): Promise<ViewDriveItemBinary> {
        return firstValueFrom(this.httpClient.get<ViewDriveItemBinary>(`${environment.Api.Service}api/v1/driveitem/binary/last/${driveitemid}`)
            .pipe(catchError(this.HandleError<ViewDriveItemBinary>('FindLatestDriveItemBinaryById', undefined))));
    }

    public FindDriveItemBinaryById(id: number): Promise<ViewDriveItemBinary> {
        return firstValueFrom(this.httpClient.get<ViewDriveItemBinary>(`${environment.Api.Service}api/v1/driveitem/binary/${id}`)
            .pipe(catchError(this.HandleError<ViewDriveItemBinary>('FindDriveItemBinaryById', undefined))));
    }

    public AddDriveItem(viewModel: AddDriveItem): Promise<ViewDriveItem> {
        return firstValueFrom(this.httpClient.post<ViewDriveItem>(`${environment.Api.Service}api/v1/driveitem/up`, viewModel)
            .pipe(catchError(this.HandleError<ViewDriveItem>('AddDriveItem', undefined))));
    }

    public RemoveDriveItemById(id: number): Promise<void> {
        return firstValueFrom(this.httpClient.delete<any>(`${environment.Api.Service}api/v1/driveitem/remove/${id}`)
            .pipe(catchError(this.HandleError<any>('RemoveDriveItemById', undefined))));
    }
}
