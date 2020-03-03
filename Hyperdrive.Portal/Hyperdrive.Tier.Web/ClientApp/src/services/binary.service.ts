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
        protected httpClient: HttpClient,
        protected matSnackBar: MatSnackBar) {
        super(httpClient, matSnackBar);
    }

    public EncodeAddArchive(viewModel: BinaryAddArchive): AddArchive {
        const resultModel: AddArchive =
        {
            By: viewModel.By,
            Data: this.ArchiveDataToBinary(viewModel.Data),
            Size: viewModel.Data.size,
            Name: viewModel.Data.name,
            Type: viewModel.Data.type
        };

        return resultModel;
    }

    public EncodeUpdateArchive(viewModel: BinaryUpdateArchive): UpdateArchive {
        const resultModel: UpdateArchive =
        {
            Id: viewModel.Id,
            By: viewModel.By,
            Data: this.ArchiveDataToBinary(viewModel.Data),
            Name: viewModel.Data.name,
            Size: viewModel.Data.size,
            Type: viewModel.Data.type
        };

        return resultModel;
    }

    public ArchiveDataToBinary(data: File): ArrayBuffer | string {

        const reader = new FileReader();

        let buffer: ArrayBuffer | string;

        reader.onloadend = () => {
            buffer = reader.result;
        };

        reader.readAsDataURL(data);

        return buffer;
    }
}
