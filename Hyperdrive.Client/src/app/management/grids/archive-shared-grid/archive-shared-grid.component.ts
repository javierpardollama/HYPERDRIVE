import {
  AfterViewInit,
  Component,
  OnDestroy,
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
import { FilterPage } from 'src/viewmodels/filters/filterpage';
import { ViewScroll } from 'src/viewmodels/views/viewscroll';

@Component({
  selector: 'app-archive-shared-grid',
  templateUrl: './archive-shared-grid.component.html',
  styleUrls: ['./archive-shared-grid.component.scss']
})
export class ArchiveSharedGridComponent implements OnInit, AfterViewInit, OnDestroy {

  public ELEMENT_DATA: ViewArchive[] = [];

  public displayedColumns: string[] = ['Id', 'Name', 'LastModified'];

  public dataSource: MatTableDataSource<ViewArchive> = new MatTableDataSource<ViewArchive>();

  public User!: ViewApplicationUser;

  public page: FilterPage =
    {
      Index: 0,
      Size: 15,
      Length: 0
    };

  // Constructor
  constructor(
    private archiveService: ArchiveService,
    public matDialog: MatDialog) {

  }

  // Life Cicle
  ngOnInit(): void {
    window.addEventListener('scroll', this.TurnThePage, true);
  }

  ngAfterViewInit(): void {
    this.GetLocalUser();
    this.FindAllSharedArchiveByApplicationUserId();
  }

  ngOnDestroy(): void {
    window.removeEventListener('scroll', this.TurnThePage, true);
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(localStorage.getItem('User') || TextAppVariants.AppEmptyCoreObject);
  }

  // Get Data from Service
  public async FindAllSharedArchiveByApplicationUserId() {
    const view = await this.archiveService.FindAllSharedArchiveByApplicationUserId(this.User.Id);

    this.ELEMENT_DATA = this.ELEMENT_DATA.concat(view);

    this.dataSource.data = this.ELEMENT_DATA;
  }


  // Filter Data
  public ApplyMyFilter(target: EventTarget | null) {
    this.dataSource.filter = (target as HTMLInputElement).value.trim().toLowerCase();
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

  private TurnThePage = async (event: Event): Promise<void> => {

    let scroll: ViewScroll = new ViewScroll(event.target as HTMLElement, this.page.Size);

    if (scroll.IsReached()) {
      this.page.Index++;
      await this.FindAllSharedArchiveByApplicationUserId();
    }
  }
}
