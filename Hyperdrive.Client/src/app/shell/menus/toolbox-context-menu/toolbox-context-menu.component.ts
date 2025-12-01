import { Component } from '@angular/core';
import { ViewLink } from '../../../../viewmodels/views/viewlink';
import { NavigationService } from '../../../../services/navigation.service';

import { Router, RouterModule } from '@angular/router';
import { MatBottomSheetModule, MatBottomSheetRef } from "@angular/material/bottom-sheet";
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
    selector: 'app-toolbox-context-menu',
    templateUrl: './toolbox-context-menu.component.html',
    styleUrl: './toolbox-context-menu.component.scss',
    imports: [
        MatListModule,
        MatButtonModule,
        MatTooltipModule,
        MatBottomSheetModule,
        RouterModule
    ]
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

    public async navigate(viewLink: ViewLink): Promise<void> {
        this.sheetRef.dismiss();
        await this.router.navigate([viewLink.Link]);
    }
}
