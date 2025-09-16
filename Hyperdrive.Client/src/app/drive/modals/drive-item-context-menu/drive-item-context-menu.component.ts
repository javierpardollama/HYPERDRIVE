import { Component, Inject } from '@angular/core';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetRef } from "@angular/material/bottom-sheet";
import { ViewDriveItem } from "../../../../viewmodels/views/viewdriveitem";
import { DriveItemService } from "../../../../services/driveitem.service";
import { BinaryService } from "../../../../services/binary.service";
import { TextAppVariants } from "../../../../variants/text.app.variants";
import { TimeAppVariants } from "../../../../variants/time.app.variants";
import { MatSnackBar } from "@angular/material/snack-bar";

@Component({
    selector: 'app-drive-item-context-menu',
    templateUrl: './drive-item-context-menu.component.html',
    styleUrl: './drive-item-context-menu.component.scss'
})
export class DriveItemContextMenuComponent {

    // Constructor
    constructor(
        public snackBar: MatSnackBar,
        public driveItemService: DriveItemService,
        public binaryService: BinaryService,
        public sheetRef: MatBottomSheetRef<DriveItemContextMenuComponent>,
        @Inject(MAT_BOTTOM_SHEET_DATA) public data: ViewDriveItem) { }

    public Share() {
        this.sheetRef.dismiss();
    }

    public async Download() {
        const binary = await this.driveItemService.FindDriveItemBinaryById(this.data.Id);
        await this.binaryService.DecodeViewDriveItem(binary);
        this.sheetRef.dismiss();
    }

    public Rename() {
        this.sheetRef.dismiss();
    }

    public Activity() {
        this.sheetRef.dismiss();
    }

    public async Remove(): Promise<void> {
        await this.driveItemService.RemoveDriveItemById(this.data.Id);
        
        this.snackBar.open(
            TextAppVariants.AppOperationSuccessCoreText,
            TextAppVariants.AppOkButtonText,
            { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

        this.sheetRef.dismiss();
    }
}
