import { Component, OnInit }
  from '@angular/core';

import { AuthService }
  from '../../../../services/auth.service';

import { ViewApplicationUser }
  from '../../../../viewmodels/views/viewapplicationuser';

import { AuthSignOut }
  from '../../../../viewmodels/auth/authsignout';

import { Router }
  from '@angular/router';

import { MatBottomSheetRef } from "@angular/material/bottom-sheet";

@Component({
  selector: 'app-profile-context-menu',
  templateUrl: './profile-context-menu.component.html',
  styleUrl: './profile-context-menu.component.scss'
})
export class ProfileContextMenuComponent implements OnInit {

  public User?: ViewApplicationUser;

  public constructor(
    public sheetRef: MatBottomSheetRef<ProfileContextMenuComponent>,
    private authService: AuthService,
    private router: Router) { }


  ngOnInit(): void {
    this.GetLocalUser()
  }

  public Security() {
    this.sheetRef.dismiss();
    this.router.navigate(['security']);
  }

  public async SignOut() {
    const viewModel: AuthSignOut =
    {
      Email: this.User!.Email
    };

    this.sheetRef.dismiss();

    sessionStorage.removeItem('User');   

    await this.authService.SignOut(viewModel);

    this.router.navigate(['']);
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(sessionStorage.getItem('User')!);
  }
}
