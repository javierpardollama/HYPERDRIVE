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
        return firstValueFrom(this.httpClient.put<ViewDriveItem>(`${environment.Api.Service}api/driveitem/updatedriveitemname`, viewModel)
            .pipe(catchError(this.HandleError<ViewDriveItem>('UpdateDriveItemName', undefined))));
    }

    public UpdateDriveItemSharedWith(viewModel: UpdateDriveItemSharedWith): Promise<ViewDriveItem> {
        return firstValueFrom(this.httpClient.put<ViewDriveItem>(`${environment.Api.Service}api/driveitem/updatedriveitemsharedwith`, viewModel)
            .pipe(catchError(this.HandleError<ViewDriveItem>('UpdateDriveItemSharedWith', undefined))));
    }

    public FindAllDriveItem(): Promise<ViewDriveItem[]> {
        return firstValueFrom(this.httpClient.get<ViewDriveItem[]>(`${environment.Api.Service}api/driveitem/findalldriveitem`)
            .pipe(catchError(this.HandleError<ViewDriveItem[]>('FindAllDriveItem', []))));
    }

    public FindPaginatedDriveItemByApplicationUserId(page: FilterPageDriveItem): Promise<ViewPage<ViewDriveItem>> {
        return firstValueFrom(this.httpClient.post<ViewPage<ViewDriveItem>>(`${environment.Api.Service}api/driveitem/findpaginateddriveitembyapplicationuserid`, page)
            .pipe(catchError(this.HandleError<ViewPage<ViewDriveItem>>('FindPaginatedDriveItemByApplicationUserId', undefined))));
    }

    public FindPaginatedSharedDriveItemByApplicationUserId(page: FilterPageDriveItem): Promise<ViewPage<ViewDriveItem>> {
        return firstValueFrom(this.httpClient.post<ViewPage<ViewDriveItem>>(`${environment.Api.Service}api/driveitem/findpaginatedshareddriveitembyapplicationuserid`, page)
            .pipe(catchError(this.HandleError<ViewPage<ViewDriveItem>>('FindPaginatedSharedDriveItemByApplicationUserId', undefined))));
    }

    public FindAllDriveItemVersionByDriveItemId(id: number): Promise<ViewDriveItemVersion[]> {
        return firstValueFrom(this.httpClient.get<ViewDriveItemVersion[]>(`${environment.Api.Service}api/driveitem/findalldriveitemversionbydriveitemid/${id}`)
            .pipe(catchError(this.HandleError<ViewDriveItemVersion[]>('FindAllDriveItemVersionByDriveItemId', []))));
    }

    public FindDriveItemBinaryByIdQuery(id: number): Promise<ViewDriveItemBinary> {
        return firstValueFrom(this.httpClient.get<ViewDriveItemBinary>(`${environment.Api.Service}api/driveitem/finddriveitembinarybyid/${id}`)
            .pipe(catchError(this.HandleError<ViewDriveItemBinary>('FindDriveItemBinaryByIdQuery', undefined))));
    }

    public AddDriveItem(viewModel: AddDriveItem): Promise<ViewDriveItem> {
        return firstValueFrom(this.httpClient.post<ViewDriveItem>(`${environment.Api.Service}api/driveitem/adddriveitem`, viewModel)
            .pipe(catchError(this.HandleError<ViewDriveItem>('AddDriveItem', undefined))));
    }

    public UpdateDriveItem(viewModel: AddDriveItem): Promise<ViewDriveItem> {
        return firstValueFrom(this.httpClient.put<ViewDriveItem>(`${environment.Api.Service}api/driveitem/updatedriveitem`, viewModel)
            .pipe(catchError(this.HandleError<ViewDriveItem>('UpdateDriveItem', undefined))));
    }

    public RemoveDriveItemById(id: number): Promise<void> {
        return firstValueFrom(this.httpClient.delete<any>(`${environment.Api.Service}api/driveitem/removedriveitembyid/${id}`)
            .pipe(catchError(this.HandleError<any>('RemoveDriveItemById', undefined))));
    }
}
