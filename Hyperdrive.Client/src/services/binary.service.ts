import { AddDriveItem } from './../viewmodels/additions/adddriveitem';

import { UpdateDriveItem } from './../viewmodels/updates/updatedriveitem';

import { BinaryAddDriveItem } from './../viewmodels/binary/binaryadddriveitem';

import { BinaryUpdateDriveItem } from './../viewmodels/binary/binaryupdatedriveitem';

import { ViewDriveItemVersion } from './../viewmodels/views/viewdriveitemversion';

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

    public async EncodeAddDriveItem(viewModel: BinaryAddDriveItem): Promise<AddDriveItem> {
        const resultModel: AddDriveItem =
        {
            ApplicationUserId: viewModel.ApplicationUserId,
            Data: await this.EncodeContent(viewModel.Data),
            Size: viewModel.Data.size,
            Name: viewModel.Data.name,
            Type: viewModel.Data.type,
            Folder: viewModel.Folder,
            Locked: viewModel.Locked
        };

        return resultModel;
    }

    public async EncodeUpdateDriveItem(viewModel: BinaryUpdateDriveItem): Promise<UpdateDriveItem> {
        const resultModel: UpdateDriveItem =
        {
            Id: viewModel.Id,
            ApplicationUserId: viewModel.ApplicationUserId,
            Data: await this.EncodeContent(viewModel.Data),
            Name: viewModel.Data.name,
            Size: viewModel.Data.size,
            Type: viewModel.Data.type,
            Folder: viewModel.Folder,
            Locked: viewModel.Locked
        };

        return resultModel;
    }

    public async DecodeViewDriveItem(viewModel: ViewDriveItemVersion): Promise<void> {

        const blob = await this.DecodeContent(viewModel.Data!, viewModel.Type);

        const url = window.URL.createObjectURL(blob);

        const anchor = Object.assign(document.createElement("a"), { style: "display:none", href: url, download: viewModel.Name });

        document.body.appendChild(anchor);
        anchor.click();

        window.URL.revokeObjectURL(url);
    }

    public async EncodeContent(file: File): Promise<string> {

        const bytes = new Uint8Array(await file.arrayBuffer());

        const binaryString = Array.from(bytes).map(byte => String.fromCharCode(byte)).join('');

        return window.btoa(binaryString);
    }

    public async DecodeContent(content: string, type: string): Promise<Blob> {

        const bytes = new Uint8Array(
            Array.from(window.atob(content!), char => char.charCodeAt(0))
        );

        return new Blob([bytes], { type: type });
    }
}
