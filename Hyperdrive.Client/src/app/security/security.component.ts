import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { ChangeEmailSecurityComponent }
  from './changeemail-security/changeemail-security.component';

import { ChangePasswordSecurityComponent }
  from './changepassword-security/changepassword-security.component';

import { ChangePhoneNumberSecurityComponent }
  from './changephonenumber-security/changephonenumber-security.component';

import { ViewApplicationUser }
  from './../../viewmodels/views/viewapplicationuser';

@Component({
  selector: 'app-security',
  templateUrl: './security.component.html',
  styleUrls: ['./security.component.scss']
})
export class SecurityComponent implements OnInit {

  public User?: ViewApplicationUser;

  // Constructor
  constructor(
    public matDialog: MatDialog) {

  }

  // Life Cicle
  ngOnInit() {
    this.GetLocalUser()
  }

  ChangeEmail() {
    const dialogRef = this.matDialog.open(ChangeEmailSecurityComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(() => {
      this.GetLocalUser();
    });
  }

  ChangePassword() {
    const dialogRef = this.matDialog.open(ChangePasswordSecurityComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(() => {
      this.GetLocalUser();
    });
  }

  ChangePhoneNumber() {
    const dialogRef = this.matDialog.open(ChangePhoneNumberSecurityComponent, {
      width: '450px'
    });

    dialogRef.afterClosed().subscribe(() => {
      this.GetLocalUser();
    });
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(sessionStorage.getItem('User')!);
  }
}
