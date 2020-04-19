import { Component, OnInit } from '@angular/core';
import { ViewLink } from './../../viewmodels/views/viewlink';
import { NavigationService } from './../../services/navigation.service';

@Component({
  selector: 'app-management',
  templateUrl: './management.component.html',
  styleUrls: ['./management.component.css']
})
export class ManagementComponent implements OnInit {

  NavigationLinks: ViewLink[];

  // Constructor
  constructor(
    private navigationService: NavigationService) {
    this.NavigationLinks = this.navigationService.GetManagementNavigationLinks();
  }

  // Life Cicle
  ngOnInit() {
  }
}