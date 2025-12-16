import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { DriveItemService } from "../../../../services/driveitem.service";
import { ViewDriveItem } from "../../../../viewmodels/views/viewdriveitem";
import { MatTableDataSource, MatTableModule } from "@angular/material/table";
import { FilterPageDriveItem } from "../../../../viewmodels/filters/filterpagedriveitem";
import { ViewScroll } from "../../../../viewmodels/views/viewscroll";
import { ViewApplicationUser } from "../../../../viewmodels/views/viewapplicationuser";
import { MatBottomSheet, MatBottomSheetModule } from "@angular/material/bottom-sheet";
import { DriveItemContextMenuComponent } from "../../menus/drive-item-context-menu/drive-item-context-menu.component";
import {
    DriveItemFolderAddModalComponent
} from "../../modals/additions/driveitem-folder-add-modal/driveitem-folder-add-modal.component";
import {
    DriveItemFileAddModalComponent
} from "../../modals/additions/driveitem-file-add-modal/driveitem-file-add-modal.component";
import { MatDialog, MatDialogModule } from "@angular/material/dialog";
import { BinaryService } from 'src/services/binary.service';
import { BinaryAddDriveItem } from 'src/viewmodels/binary/binaryadddriveitem';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TextAppVariants } from 'src/variants/text.app.variants';
import { TimeAppVariants } from 'src/variants/time.app.variants';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatChipsModule } from '@angular/material/chips';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { DragDropDirective } from 'src/directives/drag-drop.directive';


@Component({
    selector: 'app-driveitem-grid',
    templateUrl: './driveitem-grid.component.html',
    styleUrl: './driveitem-grid.component.scss',
    imports: [
        MatTableModule,
        MatDialogModule,
        FormsModule,
        MatButtonModule,
        MatTooltipModule,
        MatChipsModule,
        MatBottomSheetModule,
        MatInputModule,
        MatMenuModule,
        MatFormFieldModule,
        CommonModule,
        MatPaginatorModule,
        MatSortModule,
        DragDropDirective
    ]
})
export class DriveitemGridComponent implements OnInit, AfterViewInit, OnDestroy {

    public loading: boolean = false;

    public ELEMENT_DATA: ViewDriveItem[] = [];

    public displayedColumns: string[] = ['Id', 'Name', 'Folder', 'LastModified', 'SharedWith'];

    public dataSource: MatTableDataSource<ViewDriveItem> = new MatTableDataSource<ViewDriveItem>();

    public page: FilterPageDriveItem =
        {
            Index: 0,
            Size: 15,
            Length: 0,
            ApplicationUserId: undefined,
            ParentId: undefined,
        };

    private User?: ViewApplicationUser;

    // Constructor
    constructor(
        private matSnackBar: MatSnackBar,
        public matDialog: MatDialog,
        private driveItemService: DriveItemService,
        private binaryService: BinaryService,
        private secureStorageService: SecureStorageService,
        public bottomSheet: MatBottomSheet) {

    }

    // Life Cicle
    ngOnInit(): void {
        window.addEventListener('scroll', this.TurnThePage, true);
    }

    async ngAfterViewInit(): Promise<void> {
        await this.GetLocalUser();
        this.SetFilterUser()
        await this.FindPaginatedDriveItem();
    }

    ngOnDestroy(): void {
        window.removeEventListener('scroll', this.TurnThePage, true);
    }

    // Get Data from Service
    public async FindPaginatedDriveItem(): Promise<void> {
        this.loading = true;
        const view = await this.driveItemService.FindPaginatedDriveItemByApplicationUserId(this.page);
        this.loading = false;

        if (view) {
            this.page.Length = view.Length;
            this.ELEMENT_DATA = Array.from(this.ELEMENT_DATA.concat(view.Items).reduce((m, t): Map<ViewDriveItem, ViewDriveItem> => m.set(t.Id, t), new Map()).values());
            this.dataSource.data = this.ELEMENT_DATA;
        }
    }

    // Filter Data
    public ApplyMyFilter(target: EventTarget | null): void {
        this.dataSource.filter = (target as HTMLInputElement).value.trim().toLowerCase();
    }

    public async ToRecord(row: ViewDriveItem): Promise<void> {
        if (row.Folder) {
            this.ELEMENT_DATA = [];

            this.page.ParentId = row.Id;

            await this.FindPaginatedDriveItem();
        }

    }

    public ToContext(event: MouseEvent, row: ViewDriveItem): void {

        event.preventDefault(); // disables the browser's right-click menu
        event.stopPropagation(); // prevents bubbling

        const sheetRef = this.bottomSheet.open(DriveItemContextMenuComponent, {
            data: row
        });

        sheetRef.afterDismissed().subscribe(async () => {
            await this.FindPaginatedDriveItem();
        });
    }

    public NewFolder(): void {
        const dialogRef = this.matDialog.open(DriveItemFolderAddModalComponent, {
            width: '450px',
            data: this.page.ParentId,
        });

        dialogRef.afterClosed().subscribe(() => {

        });
    }

    public NewFile(): void {
        const dialogRef = this.matDialog.open(DriveItemFileAddModalComponent, {
            width: '450px',
            data: this.page.ParentId,
        });

        dialogRef.afterClosed().subscribe(() => {

        });
    }

    public async DropFile(event: DragEvent): Promise<void> {
        if (event?.dataTransfer?.files?.length) {

            let binaries = Array.from(event.dataTransfer.files).map(file => ({
                ApplicationUserId: this.page.ApplicationUserId!,
                File: file,
                ParentId: this.page.ParentId,
                Folder: false
            } as BinaryAddDriveItem));

            await Promise.all(
                binaries.map(async binary => {
                    let viewModel = await this.binaryService.EncodeDriveItem(binary);

                    let archive = await this.driveItemService.AddDriveItem(viewModel);
                    if (archive)
                        this.matSnackBar.open(
                            TextAppVariants.AppOperationSuccessCoreText,
                            TextAppVariants.AppOkButtonText,
                            { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
                })
            );
        }
    }

    private TurnThePage = async (event: Event): Promise<void> => {

        let scroll: ViewScroll = new ViewScroll(event.target as HTMLElement, this.page.Size, this.ELEMENT_DATA.length, this.page.Length);

        if (scroll.IsReached()) {
            this.page.Index++;
            await this.FindPaginatedDriveItem();
        }
    }

    public async GetLocalUser(): Promise<void> {
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>('User');;
    }

    public SetFilterUser(): void {
        this.page.ApplicationUserId = this.User?.Id;
    }
}
