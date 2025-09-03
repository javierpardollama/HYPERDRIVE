import { AddApplicationRole } from '../viewmodels/additions/addapplicationrole';

import { UpdateApplicationRole } from '../viewmodels/updates/updateapplicationrole';

import { ViewPage } from '../viewmodels/views/viewpage';

import { ViewApplicationRole } from '../viewmodels/views/viewapplicationrole';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { FilterPageApplicationRole } from '../viewmodels/filters/filterpageapplicationrole';

import { firstValueFrom } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';

@Injectable({
    providedIn: 'root',
})

export class ApplicationRoleService extends BaseService {

    public constructor(
        protected override httpClient: HttpClient,
        protected override matSnackBar: MatSnackBar,
        protected override router: Router) {
        super(httpClient, matSnackBar, router);
    }

    public UpdateApplicationRole(viewModel: UpdateApplicationRole): Promise<ViewApplicationRole> {
        return firstValueFrom(this.httpClient.put<ViewApplicationRole>(`${environment.Api.Service}api/applicationrole/updateapplicationrole`, viewModel)
            .pipe(catchError(this.HandleError<ViewApplicationRole>('UpdateApplicationRole', undefined))));
    }

    public FindAllApplicationRole(): Promise<ViewApplicationRole[]> {
        return firstValueFrom(this.httpClient.get<ViewApplicationRole[]>(`${environment.Api.Service}api/applicationrole/findallapplicationrole`)
            .pipe(catchError(this.HandleError<ViewApplicationRole[]>('FindAllApplicationRole', []))));
    }

    public FindPaginatedApplicationRole(viewModel: FilterPageApplicationRole): Promise<ViewPage<ViewApplicationRole>> {
        return firstValueFrom(this.httpClient.post<ViewPage<ViewApplicationRole>>(`${environment.Api.Service}api/applicationrole/findpaginatedapplicationrole`, viewModel)
            .pipe(catchError(this.HandleError<ViewPage<ViewApplicationRole>>('FindPaginatedApplicationRole', undefined))));
    }

    public AddApplicationRole(viewModel: AddApplicationRole): Promise<ViewApplicationRole> {
        return firstValueFrom(this.httpClient.post<ViewApplicationRole>(`${environment.Api.Service}api/applicationrole/addapplicationrole`, viewModel)
            .pipe(catchError(this.HandleError<ViewApplicationRole>('AddApplicationRole', undefined))));
    }

    public RemoveApplicationRoleById(id: number): Promise<any> {
        return firstValueFrom(this.httpClient.delete<any>(`${environment.Api.Service}api/applicationrole/removeapplicationrolebyid/${id}`)
            .pipe(catchError(this.HandleError<any>('RemoveApplicationRoleById', undefined))));
    }
}