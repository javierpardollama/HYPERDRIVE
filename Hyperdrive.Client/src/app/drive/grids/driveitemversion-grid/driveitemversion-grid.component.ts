import { CommonModule } from '@angular/common';
import { AfterViewInit, ChangeDetectionStrategy, Component, inject, OnDestroy, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ViewScroll } from 'src/viewmodels/views/viewscroll';
import { ViewDriveItemVersion } from 'src/viewmodels/views/viewdriveitemversion';
import { FilterPageDriveItemVersion } from 'src/viewmodels/filters/filterpagedriveitemversion';
import { FormsModule } from '@angular/forms';
import { DriveItemVersionService } from 'src/services/driveitemversion.service';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-driveitemversion-grid',
  imports: [
    MatTableModule,
    FormsModule,
    MatButtonModule,
    MatTooltipModule,
    MatInputModule,
    MatFormFieldModule,
    CommonModule,
    MatPaginatorModule,
    MatSortModule,
  ],
  templateUrl: './driveitemversion-grid.component.html',
  styleUrl: './driveitemversion-grid.component.scss',
})
export class DriveitemversionGridComponent implements OnInit, AfterViewInit, OnDestroy {
  // DI
  matDialog = inject(MatDialog);
  private driveItemVersionService = inject(DriveItemVersionService);

  public loading: boolean = false;

  public ELEMENT_DATA: ViewDriveItemVersion[] = [];

  public displayedColumns: string[] = ['Id', 'FileName', 'Size', 'Type', 'LastModified'];

  public dataSource: MatTableDataSource<ViewDriveItemVersion> = new MatTableDataSource<ViewDriveItemVersion>();

  public page: FilterPageDriveItemVersion =
    {
      Index: 0,
      Size: 15,
      Length: 0,
      Id: undefined
    };

  // Constructor
  constructor() {

  }

  // Life Cicle
  ngOnInit(): void {
    window.addEventListener('scroll', this.TurnThePage, true);
  }

  async ngAfterViewInit(): Promise<void> {
    await this.FindPaginatedDriveItemVersionByDriveItemId();
  }

  ngOnDestroy(): void {
    window.removeEventListener('scroll', this.TurnThePage, true);
  }

  // Get Data from Service
  public async FindPaginatedDriveItemVersionByDriveItemId(): Promise<void> {
    this.loading = true;
    const view = await this.driveItemVersionService.FindPaginatedDriveItemVersionByDriveItemId(this.page);
    this.loading = false;

    if (view) {
      this.page.Length = view.Length;
      this.ELEMENT_DATA = Array.from(this.ELEMENT_DATA.concat(view.Items).reduce((m, t): Map<ViewDriveItemVersion, ViewDriveItemVersion> => m.set(t.Id, t), new Map()).values());
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
      await this.FindPaginatedDriveItemVersionByDriveItemId();
    }
  }
}
