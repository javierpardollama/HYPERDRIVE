import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';

import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { ApplicationUserService } from '../../../../services/applicationuser.service';

import {
    ApplicationUserUpdateModalComponent
} from '../../modals/updates/applicationuser-update-modal/applicationuser-update-modal.component';

import { FilterPageApplicationUser } from 'src/viewmodels/filters/filterpageapplicationuser';
import { ViewScroll } from 'src/viewmodels/views/viewscroll';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatChipsModule } from '@angular/material/chips';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';


@Component({
    selector: 'app-applicationuser-grid',
    templateUrl: './applicationuser-grid.component.html',
    styleUrls: ['./applicationuser-grid.component.scss'],
    imports: [
        MatTableModule,
        MatDialogModule,
        FormsModule,
        MatButtonModule,
        MatTooltipModule,
        MatChipsModule,
        MatFormFieldModule,
        CommonModule,
        MatPaginatorModule,
        MatSortModule,
        MatInputModule,
    ]
})
export class ApplicationUserGridComponent implements OnInit, AfterViewInit, OnDestroy {

    public loading: boolean = false;

    public ELEMENT_DATA: ViewApplicationUser[] = [];

    public displayedColumns: string[] = ['Id', 'FirstName', 'ApplicationRoles', 'LastModified'];

    public dataSource: MatTableDataSource<ViewApplicationUser> = new MatTableDataSource<ViewApplicationUser>();

    public page: FilterPageApplicationUser =
        {
            Index: 0,
            Size: 15,
            Length: 0
        };

    // Constructor
    constructor(
        private applicationUserService: ApplicationUserService,
        public matDialog: MatDialog) {
    }

    // Life Cicle
    ngOnInit(): void {
        window.addEventListener('scroll', this.TurnThePage, true);
    }

    ngAfterViewInit(): void {
        this.FindPaginatedApplicationUser();
    }

    ngOnDestroy(): void {
        window.removeEventListener('scroll', this.TurnThePage, true);
    }

    // Get Data from Service
    public async FindPaginatedApplicationUser(): Promise<void> {
        this.loading = true;
        const view = await this.applicationUserService.FindPaginatedApplicationUser(this.page);
        this.loading = false;

        if (view) {
            this.page.Length = view.Length;
            this.ELEMENT_DATA = Array.from(this.ELEMENT_DATA.concat(view.Items).reduce((m, t): Map<ViewApplicationUser, ViewApplicationUser> => m.set(t.Id, t), new Map()).values());
            this.dataSource.data = this.ELEMENT_DATA;
        }
    }

    // Filter Data
    public ApplyMyFilter(target: EventTarget | null): void {
        this.dataSource.filter = (target as HTMLInputElement).value.trim().toLowerCase();
    }

    // Get Record from Table
    public GetRecord(row: ViewApplicationUser): void {
        const dialogRef = this.matDialog.open(ApplicationUserUpdateModalComponent, {
            width: '450px',
            data: row
        });

        dialogRef.afterClosed().subscribe(async () => {
            await this.FindPaginatedApplicationUser();
        });
    }


    private TurnThePage = async (event: Event): Promise<void> => {

        let scroll: ViewScroll = new ViewScroll(event.target as HTMLElement, this.page.Size, this.ELEMENT_DATA.length, this.page.Length);

        if (scroll.IsReached()) {
            this.page.Index++;
            await this.FindPaginatedApplicationUser();
        }
    }
}
