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

@Component({
  selector: 'app-archive-grid',
  templateUrl: './archive-grid.component.html',
  styleUrls: ['./archive-grid.component.css']
})
export class ArchiveGridComponent implements OnInit {

  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;

  public ELEMENT_DATA: ViewArchive[];

  public displayedColumns: string[] = ['Id', 'Name', 'LastModified'];

  public dataSource: MatTableDataSource<ViewArchive>;

  public User: ViewApplicationUser;

  // Constructor
  constructor(
    private archiveService: ArchiveService,
    public matDialog: MatDialog) {

  }

  // Life Cicle
  ngOnInit() {
    this.GetLocalUser();
    this.FindAllArchiveByApplicationUserId();
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(localStorage.getItem('User'));
  }

  // Get Data from Service
  public FindAllArchiveByApplicationUserId() {
    this.archiveService.FindAllArchiveByApplicationUserId(this.User.Id).subscribe(poblaciones => {
      this.ELEMENT_DATA = poblaciones;

      this.SetupMyTableSettings();
    });
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
      this.FindAllArchiveByApplicationUserId();
    });
  }

  public AddRecord() {
    const dialogRef = this.matDialog.open(ArchiveAddModalComponent, {
      width: '450px',
    });

    dialogRef.afterClosed().subscribe(result => {
      this.FindAllArchiveByApplicationUserId();
    });
  }
}
