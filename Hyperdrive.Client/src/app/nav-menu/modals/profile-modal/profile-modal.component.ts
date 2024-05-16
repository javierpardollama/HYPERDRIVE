import { Component, OnInit }
  from '@angular/core';

import { AuthService }
  from './../../../../services/auth.service';

import { ViewApplicationUser }
  from './../../../../viewmodels/views/viewapplicationuser';

import { AuthSignOut }
  from './../../../../viewmodels/auth/authsignout';

import { Router }
  from '@angular/router';

import { MatDialogRef }
  from '@angular/material/dialog';

@Component({
  selector: 'app-profile-modal',
  templateUrl: './profile-modal.component.html',
  styleUrl: './profile-modal.component.scss'
})
export class ProfileModalComponent implements OnInit {

  public User?: ViewApplicationUser;

  public constructor(
    public dialogRef: MatDialogRef<ProfileModalComponent>,
    private authService: AuthService,
    private router: Router) { }


  ngOnInit(): void {
    this.GetLocalUser()
  }

  public Security() {
    this.dialogRef.close();
    this.router.navigate(['security']);
  }

  public async SignOut() {
    const viewModel: AuthSignOut =
    {
      Email: this.User!.Email
    };

    this.dialogRef.close();

    sessionStorage.removeItem('User');   

    await this.authService.SignOut(viewModel);

    this.router.navigate(['']);
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(sessionStorage.getItem('User')!);
  }
}
