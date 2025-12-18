import { Injectable } from '@angular/core';

import {
    Router
} from '@angular/router';

import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { IsEmpty } from 'src/utils/object.utils';
import { VAULT_USER_KEY } from 'src/variants/vault.keys.variants';

@Injectable({
    providedIn: 'root'
})

export class SignInGuard {

    private User?: ViewApplicationUser;

    private Activated = false;

    constructor(
        private secureStorageService: SecureStorageService,
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
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>(VAULT_USER_KEY);
    }
}
