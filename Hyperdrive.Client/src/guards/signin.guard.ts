import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CryptoService } from 'src/services/crypto.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';
import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';
import { IsEmpty } from 'src/utils/object.utils';

export const SignInGuard: CanActivateFn = async () => {
    const cryptoService = inject(CryptoService);
    const router = inject(Router);

    // Get User from Storage
    const user = await cryptoService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);

    if (IsEmpty(user)) {
        return router.createUrlTree(['/auth/signin']);
    }

    return true;
};
