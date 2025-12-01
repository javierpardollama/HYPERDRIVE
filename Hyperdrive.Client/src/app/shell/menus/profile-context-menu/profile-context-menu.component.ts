import { Component, OnInit } from '@angular/core';

import { AuthService } from '../../../../services/auth.service';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { AuthSignOut } from '../../../../viewmodels/auth/authsignout';

import { Router, RouterModule } from '@angular/router';

import { MatBottomSheetModule, MatBottomSheetRef } from "@angular/material/bottom-sheet";
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SessionVaultService } from 'src/services/session.vault.service';


@Component({
    selector: 'app-profile-context-menu',
    templateUrl: './profile-context-menu.component.html',
    styleUrl: './profile-context-menu.component.scss',
    imports: [
        MatListModule,
        MatButtonModule,
        MatTooltipModule,
        MatBottomSheetModule,
        RouterModule
    ]
})
export class ProfileContextMenuComponent implements OnInit {

    public User?: ViewApplicationUser;

    public constructor(
        public sheetRef: MatBottomSheetRef<ProfileContextMenuComponent>,
        private authService: AuthService,
        private sessionVaultService: SessionVaultService,
        private router: Router) {
    }


    async ngOnInit(): Promise<void> {
        await this.GetLocalUser()
    }

    public async Security(): Promise<void> {
        this.sheetRef.dismiss();
        await this.router.navigate(['security']);
    }

    public async SignOut(): Promise<void> {
        const viewModel: AuthSignOut =
        {
            Email: this.User!.Email
        };

        this.sheetRef.dismiss();

        await this.authService.SignOut(viewModel);

        this.sessionVaultService.ClearUser();

        await this.router.navigate(['']);
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.sessionVaultService.DecryptUser();;
    }
}
