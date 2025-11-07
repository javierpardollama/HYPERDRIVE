import { Component, Inject } from '@angular/core';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetRef } from "@angular/material/bottom-sheet";
import { ViewDriveItem } from "../../../../viewmodels/views/viewdriveitem";
import { DriveItemService } from "../../../../services/driveitem.service";
import { BinaryService } from "../../../../services/binary.service";
import { TextAppVariants } from "../../../../variants/text.app.variants";
import { TimeAppVariants } from "../../../../variants/time.app.variants";
import { MatSnackBar } from "@angular/material/snack-bar";
import { MatDialog } from "@angular/material/dialog";
import {
    DriveitemNameUpdateModalComponent
} from "../../modals/updates/driveitem-name-update-modal/driveitem-name-update-modal.component";
import {
    DriveitemShareWithUpdateModalComponent
} from "../../modals/updates/driveitem-share-with-update-modal/driveitem-share-with-update-modal.component";

@Component({
    selector: 'app-drive-item-context-menu',
    templateUrl: './drive-item-context-menu.component.html',
    styleUrl: './drive-item-context-menu.component.scss'
})
export class DriveItemContextMenuComponent {

    // Constructor
    constructor(
        public matDialog: MatDialog,
        public snackBar: MatSnackBar,
        public driveItemService: DriveItemService,
        public binaryService: BinaryService,
        public sheetRef: MatBottomSheetRef<DriveItemContextMenuComponent>,
        @Inject(MAT_BOTTOM_SHEET_DATA) public data: ViewDriveItem) {
    }

    public Share(): void {
        const dialogRef = this.matDialog.open(DriveitemShareWithUpdateModalComponent, {
            data: this.data,
            width: '450px',
        });

        dialogRef.afterClosed().subscribe(() => {
            this.sheetRef.dismiss();
        });
    }

    public async Download(): Promise<void> {
        const binary = await this.driveItemService.FindDriveItemBinaryById(this.data.Id);
        await this.binaryService.DecodeViewDriveItem(binary);
        this.sheetRef.dismiss();
    }

    public Rename(): void {
        const dialogRef = this.matDialog.open(DriveitemNameUpdateModalComponent, {
            data: this.data,
            width: '450px'
        });

        dialogRef.afterClosed().subscribe(() => {
            this.sheetRef.dismiss();
        });
    }

    public Activity(): void {
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
