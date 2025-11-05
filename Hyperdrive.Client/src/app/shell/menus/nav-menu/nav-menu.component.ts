import { Component, OnInit } from '@angular/core';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';
import { ProfileContextMenuComponent } from '../profile-context-menu/profile-context-menu.component';
import { ToolboxContextMenuComponent } from '../toolbox-context-menu/toolbox-context-menu.component';
import { MatBottomSheet } from "@angular/material/bottom-sheet";
import { Decrypt } from 'src/services/crypto.sevice';

@Component({
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit {

    public User?: ViewApplicationUser;

    // Constructor
    constructor(public bottomSheet: MatBottomSheet) {

    }

    // Life Cicle
    ngOnInit(): void {
        this.GetLocalUser();
    }

    // Nav Actions
    public Profile(): void {
        const dialogRef = this.bottomSheet.open(ProfileContextMenuComponent, {});

        dialogRef.afterDismissed().subscribe(() => {
            this.GetLocalUser();
        });
    }

    public Toolbox(): void {
        const dialogRef = this.bottomSheet.open(ToolboxContextMenuComponent, {});

        dialogRef.afterDismissed().subscribe(() => {
            this.GetLocalUser();
        });
    }


    // Get User from Storage
    public GetLocalUser(): void {
        this.User = Decrypt(sessionStorage.getItem('User')!) as ViewApplicationUser;
    }
}
