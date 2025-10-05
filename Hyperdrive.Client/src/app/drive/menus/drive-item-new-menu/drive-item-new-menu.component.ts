import {Component} from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import {
    DriveItemFileAddModalComponent
} from "../../modals/additions/driveitem-file-add-modal/driveitem-file-add-modal.component";
import {
    DriveItemFolderAddModalComponent
} from "../../modals/additions/driveitem-folder-add-modal/driveitem-folder-add-modal.component";

@Component({
    selector: 'app-drive-item-new-menu',
    templateUrl: './drive-item-new-menu.component.html',
    styleUrl: './drive-item-new-menu.component.scss'
})
export class DriveItemNewMenuComponent {

    constructor(public matDialog: MatDialog) {
    }

    public NewFolder() {
        const dialogRef = this.matDialog.open(DriveItemFolderAddModalComponent, {
            width: '450px'
        });

        dialogRef.afterClosed().subscribe(() => {

        });
    }

    public NewFile() {
        const dialogRef = this.matDialog.open(DriveItemFileAddModalComponent, {
            width: '450px'
        });

        dialogRef.afterClosed().subscribe(() => {

        });
    }

}
