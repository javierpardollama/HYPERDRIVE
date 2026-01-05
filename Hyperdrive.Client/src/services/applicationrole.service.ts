import { AddApplicationRole } from '../viewmodels/additions/addapplicationrole';

import { UpdateApplicationRole } from '../viewmodels/updates/updateapplicationrole';

import { ViewPage } from '../viewmodels/views/viewpage';

import { ViewApplicationRole } from '../viewmodels/views/viewapplicationrole';

import { Injectable } from '@angular/core';

import { catchError, shareReplay } from 'rxjs/operators';

import { BaseService } from './base.service';

import { FilterPageApplicationRole } from '../viewmodels/filters/filterpageapplicationrole';

import { firstValueFrom } from 'rxjs';

import { environment } from 'src/environments/environment';
import { ViewCatalog } from "../viewmodels/views/viewcatalog";

@Injectable({
    providedIn: 'root',
})

export class ApplicationRoleService extends BaseService {

    public constructor() {
        super();
    }

    public UpdateApplicationRole(viewModel: UpdateApplicationRole): Promise<ViewApplicationRole> {
        return firstValueFrom(this.httpClient.put<ViewApplicationRole>(`${environment.Api.Service}api/v1/applicationrole/update`, viewModel)
            .pipe(catchError(this.HandleError<ViewApplicationRole>('UpdateApplicationRole', undefined))));
    }

    public FindAllApplicationRole(): Promise<ViewCatalog[]> {
        return firstValueFrom(this.httpClient.get<ViewCatalog[]>(`${environment.Api.Service}api/v1/applicationrole/all`)
            .pipe(
                // Cache the latest emission
                shareReplay({ bufferSize: 1, refCount: true }),
                catchError(this.HandleError<ViewCatalog[]>('FindAllApplicationRole', []))));
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