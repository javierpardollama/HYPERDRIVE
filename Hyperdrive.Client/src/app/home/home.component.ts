import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { DriveitemGridComponent } from '../drive/grids/driveitem-grid/driveitem-grid.component';
import { DriveItemSharedGridComponent } from '../drive/grids/drive-item-shared-grid/drive-item-shared-grid.component';

@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [
    MatTabsModule,
    DriveitemGridComponent,
    DriveItemSharedGridComponent
  ]
})
export class HomeComponent implements OnInit {

  // Constructor
  constructor() {
  }

  // Life Cicle
  ngOnInit() {
  }
}