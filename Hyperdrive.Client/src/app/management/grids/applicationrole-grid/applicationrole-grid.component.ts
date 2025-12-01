import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit
} from '@angular/core';

import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';

import { ViewApplicationRole } from '../../../../viewmodels/views/viewapplicationrole';

import { ApplicationRoleService } from '../../../../services/applicationrole.service';

import {
  ApplicationRoleUpdateModalComponent
} from '../../modals/updates/applicationrole-update-modal/applicationrole-update-modal.component';

import {
  ApplicationRoleAddModalComponent
} from '../../modals/additions/applicationrole-add-modal/applicationrole-add-modal.component';

import { FilterPageApplicationRole } from 'src/viewmodels/filters/filterpageapplicationrole';
import { ViewScroll } from 'src/viewmodels/views/viewscroll';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-applicationrole-grid',
  templateUrl: './applicationrole-grid.component.html',
  styleUrls: ['./applicationrole-grid.component.scss'],
  imports: [
    MatTableModule,
    MatDialogModule,
    FormsModule,
    MatButtonModule,
    MatTooltipModule,
    MatFormFieldModule,
    CommonModule
  ]
})
export class ApplicationRoleGridComponent implements OnInit, AfterViewInit, OnDestroy {

  public loading: boolean = false;

  public ELEMENT_DATA: ViewApplicationRole[] = [];

  public displayedColumns: string[] = ['Id', 'Name', 'ImageUri', 'LastModified'];

  public dataSource: MatTableDataSource<ViewApplicationRole> = new MatTableDataSource<ViewApplicationRole>();

  public page: FilterPageApplicationRole =
    {
      Index: 0,
      Size: 15,
      Length: 0
    };

  // Constructor
  constructor(
    private applicationRoleService: ApplicationRoleService,
    public matDialog: MatDialog) {

  }

  // Life Cicle
  ngOnInit(): void {
    window.addEventListener('scroll', this.TurnThePage, true);
  }

  ngAfterViewInit(): void {
    this.FindPaginatedApplicationRole();
  }

  ngOnDestroy(): void {
    window.removeEventListener('scroll', this.TurnThePage, true);
  }

  // Get Data from Service
  public async FindPaginatedApplicationRole(): Promise<void> {
    this.loading = true;
    const view = await this.applicationRoleService.FindPaginatedApplicationRole(this.page);
    this.loading = false;

    if (view) {
      this.page.Length = view?.Length;
      this.ELEMENT_DATA = Array.from(this.ELEMENT_DATA.concat(view?.Items).reduce((m, t): Map<ViewApplicationRole, ViewApplicationRole> => m.set(t?.Id, t), new Map()).values());
      this.dataSource.data = this.ELEMENT_DATA;
    }
  }

  // Filter Data
  public ApplyMyFilter(target: EventTarget | null): void {
    this.dataSource.filter = (target as HTMLInputElement).value.trim().toLowerCase();
  }

  // Get Record from Table
  public GetRecord(row: ViewApplicationRole): void {
    const dialogRef = this.matDialog.open(ApplicationRoleUpdateModalComponent, {
      width: '450px',
      data: row
    });

    dialogRef.afterClosed().subscribe(() => {
      this.FindPaginatedApplicationRole();
    });
  }

  public AddRecord(): void {
    const dialogRef = this.matDialog.open(ApplicationRoleAddModalComponent, {
      width: '450px',
    });

    dialogRef.afterClosed().subscribe(async () => {
      await this.FindPaginatedApplicationRole();
    });
  }

  private TurnThePage = async (event: Event): Promise<void> => {

    let scroll: ViewScroll = new ViewScroll(event.target as HTMLElement, this.page.Size, this.ELEMENT_DATA.length, this.page.Length);

    if (scroll.IsReached()) {
      this.page.Index++;
      await this.FindPaginatedApplicationRole();
    }
  }
}
