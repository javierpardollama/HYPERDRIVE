import {
  Component,
  OnInit,
  ViewChild
} from '@angular/core';

import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';

import { ViewArchive } from './../../../../viewmodels/views/viewarchive';

import { ViewApplicationUser } from './../../../../viewmodels/views/viewapplicationuser';

import { ArchiveService } from './../../../../services/archive.service';

import {
  ArchiveUpdateModalComponent
} from './../../modals/updates/archive-update-modal/archive-update-modal.component';

import {
  ArchiveAddModalComponent
} from './../../modals/additions/archive-add-modal/archive-add-modal.component';
import { TextAppVariants } from '../../../../variants/text.app.variants';

@Component({
  selector: 'app-archive-shared-grid',
  templateUrl: './archive-shared-grid.component.html',
  styleUrls: ['./archive-shared-grid.component.scss']
})
export class ArchiveSharedGridComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator!: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort!: MatSort;

  public ELEMENT_DATA: ViewArchive[] = [];

  public displayedColumns: string[] = ['Id', 'Name', 'LastModified'];

  public dataSource: MatTableDataSource<ViewArchive> = new MatTableDataSource<ViewArchive>();

  public User!: ViewApplicationUser;

  // Constructor
  constructor(
    private archiveService: ArchiveService,
    public matDialog: MatDialog) {

  }

  // Life Cicle
  ngOnInit() {
    this.GetLocalUser();
    this.FindAllSharedArchiveByApplicationUserId();
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(localStorage.getItem('User') || TextAppVariants.AppEmptyCoreObject);
  }

  // Get Data from Service
  public async FindAllSharedArchiveByApplicationUserId() {
    this.ELEMENT_DATA = await this.archiveService.FindAllSharedArchiveByApplicationUserId(this.User.Id);

    this.SetupMyTableSettings();
  }

  // Setup Table Settings
  public SetupMyTableSettings() {
    this.dataSource = new MatTableDataSource(this.ELEMENT_DATA);

    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  // Filter Data
  public ApplyMyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  // Get Record from Table
  public GetRecord(row: ViewArchive) {
    const dialogRef = this.matDialog.open(ArchiveUpdateModalComponent, {
      width: '450px',
      data: row
    });

    dialogRef.afterClosed().subscribe(result => {
      this.FindAllSharedArchiveByApplicationUserId();
    });
  }

  public AddRecord() {
    const dialogRef = this.matDialog.open(ArchiveAddModalComponent, {
      width: '450px',
    });

    dialogRef.afterClosed().subscribe(result => {
      this.FindAllSharedArchiveByApplicationUserId();
    });
  }
}
