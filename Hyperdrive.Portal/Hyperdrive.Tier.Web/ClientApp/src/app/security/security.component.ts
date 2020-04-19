import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import {
  ChangeEmailSecurityComponent
} from './../security/changeemail-security/changeemail-security.component';

import {
  ChangePasswordSecurityComponent
} from './../security/changepassword-security/changepassword-security.component';

@Component({
  selector: 'app-security',
  templateUrl: './security.component.html',
  styleUrls: ['./security.component.css']
})
export class SecurityComponent implements OnInit {

  // Constructor
  constructor(
    public matDialog: MatDialog) {

  }

  // Life Cicle
  ngOnInit() {
  }

  ChangeEmail()
  {
    const dialogRef = this.matDialog.open(ChangeEmailSecurityComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(result => {
     
    });
  }

  ChangePassword()
  {
    const dialogRef = this.matDialog.open(ChangePasswordSecurityComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(result => {
     
    });
  }
}
