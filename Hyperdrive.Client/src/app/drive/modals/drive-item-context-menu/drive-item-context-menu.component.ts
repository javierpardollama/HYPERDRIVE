import { Component, Inject } from '@angular/core';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetRef } from "@angular/material/bottom-sheet";
import { ViewDriveItem } from "../../../../viewmodels/views/viewdriveitem";
import { DriveItemService } from "../../../../services/driveitem.service";
import { BinaryService } from "../../../../services/binary.service";

@Component({
    selector: 'app-drive-item-context-menu',
    templateUrl: './drive-item-context-menu.component.html',
    styleUrl: './drive-item-context-menu.component.scss'
})
export class DriveItemContextMenuComponent {

    // Constructor
    constructor(public driveItemService: DriveItemService,
        public binaryService: BinaryService,
        public dialogRef: MatBottomSheetRef<DriveItemContextMenuComponent>,
        @Inject(MAT_BOTTOM_SHEET_DATA) public data: ViewDriveItem) { }

    public Share() {

    }

    public async Download() {
        const binary = await this.driveItemService.FindDriveItemBinaryById(this.data.Id);
        await this.binaryService.DecodeViewDriveItem(binary);
    }

    public Rename() {

    }

    public Activity() {

    }

    public Remove() {

    }
}
