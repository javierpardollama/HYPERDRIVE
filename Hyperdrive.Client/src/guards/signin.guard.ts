import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';
import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';
import { IsEmpty } from 'src/utils/object.utils';

export const SignInGuard: CanActivateFn = async () => {
    const secureStorageService = inject(SecureStorageService);
    const router = inject(Router);

    // Get User from Storage
    const user = await secureStorageService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);

    if (IsEmpty(user)) {
        return router.createUrlTree(['/auth/signin']);
    }

    return true;
};
