import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { DriveItemService } from "../../../../services/driveitem.service";
import { ViewDriveItem } from "../../../../viewmodels/views/viewdriveitem";
import { MatTableDataSource } from "@angular/material/table";
import { FilterPageDriveItem } from "../../../../viewmodels/filters/filterpagedriveitem";
import { ViewScroll } from "../../../../viewmodels/views/viewscroll";
import { ViewApplicationUser } from "../../../../viewmodels/views/viewapplicationuser";
import { MatBottomSheet } from "@angular/material/bottom-sheet";
import { DriveItemContextMenuComponent } from "../../modals/drive-item-context-menu/drive-item-context-menu.component";

@Component({
    selector: 'app-driveitem-grid',
    templateUrl: './driveitem-grid.component.html',
    styleUrl: './driveitem-grid.component.scss'
})
export class DriveitemGridComponent implements OnInit, AfterViewInit, OnDestroy {
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
        private driveItemService: DriveItemService,
        public bottomSheet: MatBottomSheet) {

    }

    // Life Cicle
    ngOnInit(): void {
        window.addEventListener('scroll', this.TurnThePage, true);
    }

    async ngAfterViewInit(): Promise<void> {
        this.GetLocalUser();
        this.SetFilterUser()
        await this.FindPaginatedDriveItem();
    }

    ngOnDestroy(): void {
        window.removeEventListener('scroll', this.TurnThePage, true);
    }

    // Get Data from Service
    public async FindPaginatedDriveItem(): Promise<void> {

        const view = await this.driveItemService.FindPaginatedDriveItemByApplicationUserId(this.page);

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

        this.page.ParentId = row.Id;

        await this.FindPaginatedDriveItem();

    }

    public ToContext(row: ViewDriveItem): void {
        const sheetRef = this.bottomSheet.open(DriveItemContextMenuComponent, {
            data: row
        });

        sheetRef.afterDismissed().subscribe(() => {
            this.FindPaginatedDriveItem();
        });
    }

    private TurnThePage = async (event: Event): Promise<void> => {

        let scroll: ViewScroll = new ViewScroll(event.target as HTMLElement, this.page.Size, this.ELEMENT_DATA.length, this.page.Length);

        if (scroll.IsReached()) {
            this.page.Index++;
            await this.FindPaginatedDriveItem();
        }
    }

    public GetLocalUser(): void {
        this.User = JSON.parse(sessionStorage.getItem('User')!);
    }

    public SetFilterUser(): void {
        this.page.ApplicationUserId = this.User?.Id;
    }
}
