import { CommonModule } from "@angular/common";
import { AfterViewInit, Component, inject, OnDestroy, OnInit } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatChipsModule } from "@angular/material/chips";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material/input";
import { MatMenuModule } from "@angular/material/menu";
import { MatPaginatorModule } from "@angular/material/paginator";
import { MatSortModule } from "@angular/material/sort";
import { MatTableDataSource, MatTableModule } from "@angular/material/table";
import { MatTooltipModule } from "@angular/material/tooltip";
import { CryptoService } from "src/services/crypto.service";
import { DriveItemService } from "src/services/driveitem.service";
import { VaultKeyAppVariants } from "src/variants/vault.keys.variants";
import { FilterPageDriveItem } from "src/viewmodels/filters/filterpagedriveitem";
import { ViewApplicationUser } from "src/viewmodels/views/viewapplicationuser";
import { ViewDriveItem } from "src/viewmodels/views/viewdriveitem";
import { ViewScroll } from "src/viewmodels/views/viewscroll";

@Component({
  selector: "app-drive-item-shared-grid",
  imports: [
        MatTableModule,
        FormsModule,
        MatButtonModule,
        MatTooltipModule,
        MatChipsModule,
        MatInputModule,
        MatMenuModule,
        MatFormFieldModule,
        CommonModule,
        MatPaginatorModule,
        MatSortModule        
    ],
  templateUrl: "./drive-item-shared-grid.component.html",
  styleUrl: "./drive-item-shared-grid.component.scss",
})
export class DriveItemSharedGridComponent implements OnInit, AfterViewInit, OnDestroy {

  // DI
    private driveItemService = inject(DriveItemService);
    private cryptoService = inject(CryptoService);

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
            ParentName: undefined
        };

    private User?: ViewApplicationUser;

    // Constructor
    constructor() {
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
        const view = await this.driveItemService.FindPaginatedSharedDriveItemByApplicationUserId(this.page);
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

    private TurnThePage = async (event: Event): Promise<void> => {

        let scroll: ViewScroll = new ViewScroll(event.target as HTMLElement, this.page.Size, this.ELEMENT_DATA.length, this.page.Length);

        if (scroll.IsReached()) {
            this.page.Index++;
            await this.FindPaginatedDriveItem();
        }
    }

    public async GetLocalUser(): Promise<void> {
        this.User = await this.cryptoService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);
    }

    public SetFilterUser(): void {
        this.page.ApplicationUserId = this.User?.Id;
    }
}