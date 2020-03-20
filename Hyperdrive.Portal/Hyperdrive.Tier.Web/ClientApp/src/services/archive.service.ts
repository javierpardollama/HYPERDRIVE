import { AddArchive } from './../viewmodels/additions/addarchive';

import { UpdateArchive } from './../viewmodels/updates/updatearchive';

import { ViewArchive } from './../viewmodels/views/viewarchive';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';
import { ViewArchiveVersion } from 'src/viewmodels/views/viewarchiveversion';

@Injectable({
    providedIn: 'root',
})

export class ArchiveService extends BaseService {

    public constructor(
        protected httpClient: HttpClient,
        protected matSnackBar: MatSnackBar) {
        super(httpClient, matSnackBar);
    }

    public UpdateArchive(viewModel: UpdateArchive): Promise<ViewArchive> {
        return this.httpClient.put<ViewArchive>('api/archive/updatearchive', viewModel)
            .pipe(catchError(this.HandleError<ViewArchive>('UpdateArchive', undefined))).toPromise();
    }

    public FindAllArchive(): Promise<ViewArchive[]> {
        return this.httpClient.get<ViewArchive[]>('api/archive/findallarchive')
            .pipe(catchError(this.HandleError<ViewArchive[]>('FindAllArchive', []))).toPromise();
    }

    public FindAllArchiveByApplicationUserId(id: number): Promise<ViewArchive[]> {
        return this.httpClient.get<ViewArchive[]>('api/archive/findallarchivebyapplicationuserid/' + id)
            .pipe(catchError(this.HandleError<ViewArchive[]>('FindAllArchiveByApplicationUserId', []))).toPromise();
    }

    public FindAllSharedArchiveByApplicationUserId(id: number): Promise<ViewArchive[]> {
        return this.httpClient.get<ViewArchive[]>('api/archive/findallsharedarchivebyapplicationuserid/' + id)
            .pipe(catchError(this.HandleError<ViewArchive[]>('FindAllSharedArchiveByApplicationUserId', []))).toPromise();
    }

    public FindAllArchiveVersionByArchiveId(id: number): Promise<ViewArchiveVersion[]> {
        return this.httpClient.get<ViewArchiveVersion[]>('api/archive/findallarchiveversionbyarchiveid/' + id)
            .pipe(catchError(this.HandleError<ViewArchiveVersion[]>('FindAllArchiveVersionByArchiveId', []))).toPromise();
    }

    public AddArchive(viewModel: AddArchive): Promise<ViewArchive> {
        return this.httpClient.post<ViewArchive>('api/archive/addarchive', viewModel)
            .pipe(catchError(this.HandleError<ViewArchive>('AddArchive', undefined))).toPromise();
    }

    public RemoveArchiveById(id: number) {
        return this.httpClient.delete<any>('api/archive/removearchivebyid/' + id)
            .pipe(catchError(this.HandleError<any>('RemoveArchiveById', undefined)));
    }
}
