import { Injectable } from '@angular/core';

import {
    Router
} from '@angular/router';

import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';
import { SessionVaultService } from 'src/services/session.vault.service';

@Injectable({
    providedIn: 'root'
})

export class SignInGuard {

    private User?: ViewApplicationUser;

    private Activated = false;

    constructor(
        private sessionVaultService: SessionVaultService,
        private router: Router
    ) { }

    async canActivate() {

        await this.GetLocalUser();

        if (!this.User) {
            this.router.navigateByUrl('auth/signin');
        } else {
            this.Activated = true;
        }

        return this.Activated;
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.sessionVaultService.DecryptUser();
    }
}
