import { Injectable, inject } from '@angular/core';

import {
    Router
} from '@angular/router';

import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { IsEmpty } from 'src/utils/object.utils';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';

@Injectable({
    providedIn: 'root'
})

export class SignInGuard {
    // DI
    private secureStorageService = inject(SecureStorageService);
    private router = inject(Router);

    private User?: ViewApplicationUser;

    private Activated = false;

    constructor() { }

    async canActivate() {

        await this.GetLocalUser();

        if (IsEmpty(this.User)) {
            this.router.navigateByUrl('auth/signin');
        } else {
            this.Activated = true;
        }

        return this.Activated;
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);
    }
}
