import { Component, OnInit } from '@angular/core';
import { ViewLink } from './../../viewmodels/views/viewlink';
import { NavigationService } from './../../services/navigation.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  NavigationLinks: ViewLink[];

  // Constructor
  constructor(
    private navigationService: NavigationService) {
    this.NavigationLinks = this.navigationService.GetHomeNavigationLinks();
  }

  // Life Cicle
  ngOnInit() {
  }
}