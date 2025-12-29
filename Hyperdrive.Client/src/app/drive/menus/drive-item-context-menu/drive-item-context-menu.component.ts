import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetModule, MatBottomSheetRef } from "@angular/material/bottom-sheet";
import { ViewDriveItem } from "../../../../viewmodels/views/viewdriveitem";
import { DriveItemService } from "../../../../services/driveitem.service";
import { BinaryService } from "../../../../services/binary.service";
import { TextAppVariants } from "../../../../variants/text.app.variants";
import { TimeAppVariants } from "../../../../variants/time.app.variants";
import { MatSnackBar, MatSnackBarModule } from "@angular/material/snack-bar";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import {
    DriveitemNameUpdateModalComponent
} from "../../modals/updates/driveitem-name-update-modal/driveitem-name-update-modal.component";
import {
    DriveitemShareWithUpdateModalComponent
} from "../../modals/updates/driveitem-share-with-update-modal/driveitem-share-with-update-modal.component";
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { DriveItemBinaryService } from 'src/services/driveitembinary.service';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-drive-item-context-menu',
    templateUrl: './drive-item-context-menu.component.html',
    styleUrl: './drive-item-context-menu.component.scss',
    imports: [
        MatListModule,
        MatButtonModule,
        MatTooltipModule,
        MatDividerModule,
        MatBottomSheetModule,
        MatSnackBarModule,
        MatDialogModule
    ]
})
export class DriveItemContextMenuComponent {
    // DI
    matDialog = inject(MatDialog);
    snackBar = inject(MatSnackBar);
    driveItemService = inject(DriveItemService);
    driveItemBinaryService = inject(DriveItemBinaryService);
    binaryService = inject(BinaryService);
    sheetRef = inject<MatBottomSheetRef<DriveItemContextMenuComponent>>(MatBottomSheetRef);
    data = inject<ViewDriveItem>(MAT_BOTTOM_SHEET_DATA);

    // Constructor
    constructor() {
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
        const binary = await this.driveItemBinaryService.FindLatestDriveItemBinaryByDriveItemId(this.data.Id);
        await this.binaryService.DecodeDriveItem(binary);
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
