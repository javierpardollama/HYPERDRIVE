import { AddDriveItem } from '../viewmodels/additions/adddriveitem';

import { BinaryAddDriveItem } from '../viewmodels/binary/binaryadddriveitem';

import { ViewDriveItemBinary } from '../viewmodels/views/viewdriveitembinary';

import { Injectable } from '@angular/core';

import { DecodeBlob, EncodeBlob } from 'src/utils/blob.utils';


@Injectable({
    providedIn: 'root',
})

export class BinaryService {

    public constructor() {
    }

    public async EncodeDriveItem(viewModel: BinaryAddDriveItem): Promise<AddDriveItem> {
        const resultModel: AddDriveItem =
        {
            ParentId: viewModel.ParentId,
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
