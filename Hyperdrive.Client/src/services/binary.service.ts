import { AddDriveItem } from '../viewmodels/additions/adddriveitem';

import { BinaryAddDriveItem } from '../viewmodels/binary/binaryadddriveitem';

import { ViewDriveItemBinary } from '../viewmodels/views/viewdriveitembinary';

import { HttpClient } from '@angular/common/http';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Injectable } from '@angular/core';

import { BaseService } from './base.service';

import { Router } from '@angular/router';

import { DecodeBlob, EncodeBlob } from 'src/utils/blob.utils';


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

    public async EncodeDriveItem(viewModel: BinaryAddDriveItem): Promise<AddDriveItem> {
        const resultModel: AddDriveItem =
        {
            ParentId:viewModel.ParentId,
            ApplicationUserId: viewModel.ApplicationUserId,
            Data: await EncodeBlob(viewModel.File),
            Size: viewModel.File.size,
            FileName: viewModel.File.name,
            Type: viewModel.File.type,
            Folder: viewModel.Folder
        };

        return resultModel;
    }

    public async DecodeDriveItem(viewModel: ViewDriveItemBinary): Promise<void> {

        const blob = await DecodeBlob(viewModel.Data!, viewModel.Type);

        const url = window.URL.createObjectURL(blob);

        const anchor = Object.assign(document.createElement("a"), { style: "display:none", href: url, download: viewModel.FileName });

        document.body.appendChild(anchor);
        anchor.click();

        window.URL.revokeObjectURL(url);
    }
}
