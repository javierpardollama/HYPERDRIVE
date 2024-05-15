import {
  Component,
  OnInit
} from '@angular/core';

import { ViewApplicationUser } from './../../viewmodels/views/viewapplicationuser';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

  public isExpanded = false;

  public isVisible = false;

  public User?: ViewApplicationUser;

  // Constructor
  constructor() {

  }

  // Life Cicle
  ngOnInit() {
    this.GetLocalUser();
  }

  // Nav Actions
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  display() {
    if (this.User) {
      this.isVisible = true;
    }

    return this.isVisible;
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(sessionStorage.getItem('User')!);
  }
}
