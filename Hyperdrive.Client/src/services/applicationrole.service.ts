import { AddApplicationRole } from '../viewmodels/additions/addapplicationrole';

import { UpdateApplicationRole } from '../viewmodels/updates/updateapplicationrole';

import { ViewPage } from '../viewmodels/views/viewpage';

import { ViewApplicationRole } from '../viewmodels/views/viewapplicationrole';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable, inject } from '@angular/core';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { FilterPageApplicationRole } from '../viewmodels/filters/filterpageapplicationrole';

import { firstValueFrom } from 'rxjs';

import { environment } from 'src/environments/environment';
import { Router } from '@angular/router';
import { ViewCatalog } from "../viewmodels/views/viewcatalog";

@Injectable({
    providedIn: 'root',
})

export class ApplicationRoleService extends BaseService {
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

    public UpdateApplicationRole(viewModel: UpdateApplicationRole): Promise<ViewApplicationRole> {
        return firstValueFrom(this.httpClient.put<ViewApplicationRole>(`${environment.Api.Service}api/v1/applicationrole/update`, viewModel)
            .pipe(catchError(this.HandleError<ViewApplicationRole>('UpdateApplicationRole', undefined))));
    }

    public FindAllApplicationRole(): Promise<ViewCatalog[]> {
        return firstValueFrom(this.httpClient.get<ViewCatalog[]>(`${environment.Api.Service}api/v1/applicationrole/all`)
            .pipe(catchError(this.HandleError<ViewCatalog[]>('FindAllApplicationRole', []))));
    }

    public FindPaginatedApplicationRole(viewModel: FilterPageApplicationRole): Promise<ViewPage<ViewApplicationRole>> {
        return firstValueFrom(this.httpClient.post<ViewPage<ViewApplicationRole>>(`${environment.Api.Service}api/v1/applicationrole/page`, viewModel)
            .pipe(catchError(this.HandleError<ViewPage<ViewApplicationRole>>('FindPaginatedApplicationRole', undefined))));
    }

    public AddApplicationRole(viewModel: AddApplicationRole): Promise<ViewApplicationRole> {
        return firstValueFrom(this.httpClient.post<ViewApplicationRole>(`${environment.Api.Service}api/v1/applicationrole/create`, viewModel)
            .pipe(catchError(this.HandleError<ViewApplicationRole>('AddApplicationRole', undefined))));
    }

    public RemoveApplicationRoleById(id: number): Promise<any> {
        return firstValueFrom(this.httpClient.delete<any>(`${environment.Api.Service}api/v1/applicationrole/remove/${id}`)
            .pipe(catchError(this.HandleError<any>('RemoveApplicationRoleById', undefined))));
    }
}