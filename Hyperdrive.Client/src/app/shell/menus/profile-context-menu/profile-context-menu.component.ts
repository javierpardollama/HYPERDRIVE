import { Component, OnInit } from '@angular/core';

import { AuthService } from '../../../../services/auth.service';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { AuthSignOut } from '../../../../viewmodels/auth/authsignout';

import { Router } from '@angular/router';

import { MatBottomSheetRef } from "@angular/material/bottom-sheet";
import { Decrypt } from 'src/services/crypto.sevice';

@Component({
    selector: 'app-profile-context-menu',
    templateUrl: './profile-context-menu.component.html',
    styleUrl: './profile-context-menu.component.scss'
})
export class ProfileContextMenuComponent implements OnInit {

    public User?: ViewApplicationUser;

    public constructor(
        public sheetRef: MatBottomSheetRef<ProfileContextMenuComponent>,
        private authService: AuthService,
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

        sessionStorage.removeItem('User');

        await this.authService.SignOut(viewModel);

        await this.router.navigate(['']);
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await Decrypt(sessionStorage.getItem('User')!) as ViewApplicationUser;
    }
}
