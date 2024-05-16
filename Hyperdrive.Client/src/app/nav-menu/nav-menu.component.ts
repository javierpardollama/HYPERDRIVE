import {
  Component,
  OnInit
} from '@angular/core';

import { MatDialog }
  from '@angular/material/dialog';

import { ViewApplicationUser }
  from './../../viewmodels/views/viewapplicationuser';
import { ProfileModalComponent } from './modals/profile-modal/profile-modal.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

  public isVisible = false;

  public User?: ViewApplicationUser;

  // Constructor
  // Constructor
  constructor(
    public matDialog: MatDialog) {

  }

  // Life Cicle
  ngOnInit() {
    this.GetLocalUser();
  }

  // Nav Actions
  Display() {
    if (this.User) {
      this.isVisible = true;
    }

    return this.isVisible;
  }

  public Profile() {
    const dialogRef = this.matDialog.open(ProfileModalComponent, {
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
