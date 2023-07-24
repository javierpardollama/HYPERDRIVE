import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit
} from '@angular/core';

import { MatDialog } from '@angular/material/dialog';
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
import { FilterPage } from './../../../../viewmodels/filters/filterpage';
import { ViewScroll } from './../../../../viewmodels/views/viewscroll';

@Component({
  selector: 'app-archive-grid',
  templateUrl: './archive-grid.component.html',
  styleUrls: ['./archive-grid.component.scss']
})
export class ArchiveGridComponent implements OnInit, AfterViewInit, OnDestroy {

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
    this.FindPaginatedArchiveByApplicationUserId();
  }

  ngOnDestroy(): void {
    window.removeEventListener('scroll', this.TurnThePage, true);
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(localStorage.getItem('User') || TextAppVariants.AppEmptyCoreObject);
  }

  // Get Data from Service
  public async FindPaginatedArchiveByApplicationUserId() {
    const view = await this.archiveService.FindPaginatedArchiveByApplicationUserId(this.User.Id);

    this.page.Length = view.Length;

    this.ELEMENT_DATA = this.ELEMENT_DATA.concat(view.Items);

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
      this.FindPaginatedArchiveByApplicationUserId();
    });
  }

  public AddRecord() {
    const dialogRef = this.matDialog.open(ArchiveAddModalComponent, {
      width: '450px',
    });

    dialogRef.afterClosed().subscribe(result => {
      this.FindPaginatedArchiveByApplicationUserId();
    });
  }

  private TurnThePage = async (event: Event): Promise<void> => {

    let scroll: ViewScroll = new ViewScroll(event.target as HTMLElement, this.page.Size);

    if (scroll.IsReached()) {
      this.page.Index++;
      await this.FindPaginatedArchiveByApplicationUserId();
    }
  }

}
