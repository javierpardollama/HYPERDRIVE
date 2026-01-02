import { UpdateApplicationUser } from '../viewmodels/updates/updateapplicationuser';

import { ViewPage } from '../viewmodels/views/viewpage';

import { ViewApplicationUser } from '../viewmodels/views/viewapplicationuser';

import { Injectable } from '@angular/core';

import { catchError } from 'rxjs/operators';

import { BaseService } from './base.service';

import { FilterPageApplicationUser } from '../viewmodels/filters/filterpageapplicationuser';

import { firstValueFrom } from 'rxjs';

import { environment } from 'src/environments/environment';
import { ViewCatalog } from "../viewmodels/views/viewcatalog";

@Injectable({
    providedIn: 'root',
})

export class ApplicationUserService extends BaseService {

    public constructor() {
        super();
    }

    public UpdateApplicationUser(viewModel: UpdateApplicationUser): Promise<ViewApplicationUser> {
        return firstValueFrom(this.httpClient.put<ViewApplicationUser>(`${environment.Api.Service}api/v1/applicationuser/update`, viewModel)
            .pipe(catchError(this.HandleError<ViewApplicationUser>('UpdateApplicationUser', undefined))));
    }

    public FindAllApplicationUser(): Promise<ViewCatalog[]> {
        return firstValueFrom(this.httpClient.get<ViewCatalog[]>(`${environment.Api.Service}api/v1/applicationuser/all`)
            .pipe(catchError(this.HandleError<ViewCatalog[]>('FindAllApplicationUser', []))));
    }

    public FindPaginatedApplicationUser(viewModel: FilterPageApplicationUser): Promise<ViewPage<ViewApplicationUser>> {
        return firstValueFrom(this.httpClient.post<ViewPage<ViewApplicationUser>>(`${environment.Api.Service}api/v1/applicationuser/page`, viewModel)
            .pipe(catchError(this.HandleError<ViewPage<ViewApplicationUser>>('FindPaginatedApplicationUser', undefined))));
    }

    public RemoveApplicationUserById(id: number): Promise<any> {
        return firstValueFrom(this.httpClient.delete<any>(`${environment.Api.Service}api/v1/applicationuser/remove/${id}`)
            .pipe(catchError(this.HandleError<any>('RemoveApplicationUserById', undefined))));
    }
}