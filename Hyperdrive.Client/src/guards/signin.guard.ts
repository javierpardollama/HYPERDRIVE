import { Injectable } from '@angular/core';

import {
    Router
} from '@angular/router';

import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';
import { SecureStorage } from 'src/services/secure.storage';
import { IsEmpty } from 'src/utils/object.utils';

@Injectable({
    providedIn: 'root'
})

export class SignInGuard {

    private User?: ViewApplicationUser;

    private Activated = false;

    constructor(
        private secureStorage: SecureStorage,
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
        this.User = await this.secureStorage.RetrieveItem<ViewApplicationUser>('User');
    }
}
