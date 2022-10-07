import { AddArchive } from './../viewmodels/additions/addarchive';

import { UpdateArchive } from './../viewmodels/updates/updatearchive';

import { BinaryAddArchive } from './../viewmodels/binary/binaryaddarchive';

import { BinaryUpdateArchive } from './../viewmodels/binary/binaryupdatearchive';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root',
})

export class BinaryService extends BaseService {

    public constructor(
        protected override httpClient: HttpClient,
        protected override matSnackBar: MatSnackBar) {
        super(httpClient, matSnackBar);
    }

    public async EncodeAddArchive(viewModel: BinaryAddArchive): Promise<AddArchive> {
        const resultModel: AddArchive =
        {
            ApplicationUserId: viewModel.ApplicationUserId,
            Data: new Int8Array(await viewModel.Data.arrayBuffer()),
            Size: viewModel.Data.size,
            Name: viewModel.Data.name,
            Type: viewModel.Data.type,
            Folder: viewModel.Folder,
            Locked: viewModel.Locked
        };

        return resultModel;
    }

    public async EncodeUpdateArchive(viewModel: BinaryUpdateArchive): Promise<UpdateArchive> {
        const resultModel: UpdateArchive =
        {
            Id: viewModel.Id,
            ApplicationUserId: viewModel.ApplicationUserId,
            Data: new Int8Array(await viewModel.Data.arrayBuffer()),
            Name: viewModel.Data.name,
            Size: viewModel.Data.size,
            Type: viewModel.Data.type,
            Folder: viewModel.Folder,
            Locked: viewModel.Locked
        };

        return resultModel;
    }
}
