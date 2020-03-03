import {
  Component,
  OnInit
} from '@angular/core';

import { ViewApplicationUser } from './../../viewmodels/views/viewapplicationuser';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  public isExpanded = false;

  public isVisible = false;

  public User: ViewApplicationUser;

  // Constructor
  constructor() {

  }

  // Life Cicle
  ngOnInit() {

  }

  // Nav Actions
  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  display() {
    this.GetLocalUser();

    if (this.User !== null) {
      this.isVisible = true;
    }

    return this.isVisible;
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(localStorage.getItem('User'));
  }
}
