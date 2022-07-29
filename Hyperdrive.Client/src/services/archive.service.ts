import { AddArchive } from './../viewmodels/additions/addarchive';

import { UpdateArchive } from './../viewmodels/updates/updatearchive';

import { ViewArchive } from './../viewmodels/views/viewarchive';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { firstValueFrom } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { ViewArchiveVersion } from 'src/viewmodels/views/viewarchiveversion';

import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root',
})

export class ArchiveService extends BaseService {

    public constructor(
        protected override httpClient: HttpClient,
        protected override matSnackBar: MatSnackBar) {
        super(httpClient, matSnackBar);
    }

    public UpdateArchive(viewModel: UpdateArchive): Promise<ViewArchive> {
        return firstValueFrom(this.httpClient.put<ViewArchive>(`${environment.ApiService}api/archive/updatearchive`, viewModel)
            .pipe(catchError(this.HandleError<ViewArchive>('UpdateArchive', undefined))));
    }

    public FindAllArchive(): Promise<ViewArchive[]> {
        return firstValueFrom(this.httpClient.get<ViewArchive[]>(`${environment.ApiService}api/archive/findallarchive`)
            .pipe(catchError(this.HandleError<ViewArchive[]>('FindAllArchive', []))));
    }

    public FindAllArchiveByApplicationUserId(id: number): Promise<ViewArchive[]> {
        return firstValueFrom(this.httpClient.get<ViewArchive[]>(`${environment.ApiService}api/archive/findallarchivebyapplicationuserid/` + id)
            .pipe(catchError(this.HandleError<ViewArchive[]>('FindAllArchiveByApplicationUserId', []))));
    }

    public FindAllSharedArchiveByApplicationUserId(id: number): Promise<ViewArchive[]> {
        return firstValueFrom(this.httpClient.get<ViewArchive[]>(`${environment.ApiService}api/archive/findallsharedarchivebyapplicationuserid/` + id)
            .pipe(catchError(this.HandleError<ViewArchive[]>('FindAllSharedArchiveByApplicationUserId', []))));
    }

    public FindAllArchiveVersionByArchiveId(id: number): Promise<ViewArchiveVersion[]> {
        return firstValueFrom(this.httpClient.get<ViewArchiveVersion[]>(`${environment.ApiService}api/archive/findallarchiveversionbyarchiveid/` + id)
            .pipe(catchError(this.HandleError<ViewArchiveVersion[]>('FindAllArchiveVersionByArchiveId', []))));
    }

    public AddArchive(viewModel: AddArchive): Promise<ViewArchive> {
        return firstValueFrom(this.httpClient.post<ViewArchive>(`${environment.ApiService}api/archive/addarchive`, viewModel)
            .pipe(catchError(this.HandleError<ViewArchive>('AddArchive', undefined))));
    }

    public RemoveArchiveById(id: number): Promise<void> {
        return firstValueFrom(this.httpClient.delete<any>(`${environment.ApiService}api/archive/removearchivebyid/` + id)
            .pipe(catchError(this.HandleError<any>('RemoveArchiveById', undefined))));
    }
}
