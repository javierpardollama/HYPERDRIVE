import { AddArchive } from './../viewmodels/additions/addarchive';

import { UpdateArchive } from './../viewmodels/updates/updatearchive';

import { BinaryAddArchive } from './../viewmodels/binary/binaryaddarchive';

import { BinaryUpdateArchive } from './../viewmodels/binary/binaryupdatearchive';

import { ViewArchiveVersion } from './../viewmodels/views/viewarchiveversion';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { BaseService } from './base.service';

import { Router } from '@angular/router';


@Injectable({
    providedIn: 'root',
})

export class BinaryService extends BaseService {

    public constructor(
        protected override httpClient: HttpClient,
        protected override matSnackBar: MatSnackBar,
        protected override router: Router) {
        super(httpClient, matSnackBar, router);
    }

    public async EncodeAddArchive(viewModel: BinaryAddArchive): Promise<AddArchive> {
        const resultModel: AddArchive =
        {
            ApplicationUserId: viewModel.ApplicationUserId,
            Data: await viewModel.Data.arrayBuffer(),
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
            Data: await viewModel.Data.arrayBuffer(),
            Name: viewModel.Data.name,
            Size: viewModel.Data.size,
            Type: viewModel.Data.type,
            Folder: viewModel.Folder,
            Locked: viewModel.Locked
        };

        return resultModel;
    }

    public async DecodeViewArchive(viewModel: ViewArchiveVersion): Promise<void> {
        const blob = new Blob([viewModel.Data!], { type: viewModel.Type });

        const url = window.URL.createObjectURL(blob);

        const anchor = Object.assign(document.createElement("a"), { style: "display:none", href: url, download: viewModel.Archive.Name });
       
        document.body.appendChild(anchor);
        anchor.click();

        window.URL.revokeObjectURL(url);
    }
}
