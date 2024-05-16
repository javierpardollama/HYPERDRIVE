import { Component } from '@angular/core';
import { ViewLink } from './../../../../viewmodels/views/viewlink';
import { NavigationService } from './../../../../services/navigation.service';
import { MatDialogRef } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-toolbox-modal',
  templateUrl: './toolbox-modal.component.html',
  styleUrl: './toolbox-modal.component.scss'
})
export class ToolBoxModalComponent {

  public NavigationLinks: ViewLink[];

  // Constructor
  constructor(
    private router: Router,
    public dialogRef: MatDialogRef<ToolBoxModalComponent>,
    private navigationService: NavigationService) {
    this.NavigationLinks = this.navigationService.GetToolBoxNavigationLinks();
  }

  public navigate(viewLink: ViewLink) {
    this.dialogRef.close();
    this.router.navigate([viewLink.Link]);
  }
}
