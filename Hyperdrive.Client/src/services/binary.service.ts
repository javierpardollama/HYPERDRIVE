import { AddDriveItem } from '../viewmodels/additions/adddriveitem';

import { BinaryAddDriveItem } from '../viewmodels/binary/binaryadddriveitem';

import { ViewDriveItemBinary } from '../viewmodels/views/viewdriveitembinary';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { BaseService } from './base.service';

import { Router } from '@angular/router';
import { Base64ToBlob, FileToBase64 } from 'src/utils/blob.utils';


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
            Data: await FileToBase64(viewModel.Data),
            Size: viewModel.Data.size,
            FileName: viewModel.Data.name,
            Type: viewModel.Data.type,
            Folder: viewModel.Folder
        };

        return resultModel;
    }

    public async DecodeViewDriveItem(viewModel: ViewDriveItemBinary): Promise<void> {

        const blob = await Base64ToBlob(viewModel.Data!, viewModel.Type);

        const url = window.URL.createObjectURL(blob);

        const anchor = Object.assign(document.createElement("a"), { style: "display:none", href: url, download: viewModel.FileName });

        document.body.appendChild(anchor);
        anchor.click();

        window.URL.revokeObjectURL(url);
    }
}
