import { Component, OnInit } from '@angular/core';
import { DriveitemGridComponent } from '../drive/grids/driveitem-grid/driveitem-grid.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  imports: [DriveitemGridComponent]
})
export class HomeComponent implements OnInit {

  // Constructor
  constructor() {
  }

  // Life Cicle
  ngOnInit() {
  }
}