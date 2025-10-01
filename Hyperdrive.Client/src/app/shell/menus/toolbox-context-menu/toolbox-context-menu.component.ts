import { Component } from '@angular/core';
import { ViewLink } from '../../../../viewmodels/views/viewlink';
import { NavigationService } from '../../../../services/navigation.service';

import { Router } from '@angular/router';
import { MatBottomSheetRef } from "@angular/material/bottom-sheet";

@Component({
  selector: 'app-toolbox-context-menu',
  templateUrl: './toolbox-context-menu.component.html',
  styleUrl: './toolbox-context-menu.component.scss'
})
export class ToolboxContextMenuComponent {

  public NavigationLinks: ViewLink[];

  // Constructor
  constructor(
    private router: Router,
    public sheetRef: MatBottomSheetRef<ToolboxContextMenuComponent>,
    private navigationService: NavigationService) {
    this.NavigationLinks = this.navigationService.GetToolBoxNavigationLinks();
  }

  public navigate(viewLink: ViewLink) {
    this.sheetRef.dismiss();
    this.router.navigate([viewLink.Link]);
  }
}
