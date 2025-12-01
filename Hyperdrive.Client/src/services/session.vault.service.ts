import { Injectable, OnDestroy } from "@angular/core";
import { CreateCryptoKey, DecryptObject, EncryptObject } from "src/utils/crypto.utils";
import { ViewApplicationUser } from "src/viewmodels/views/viewapplicationuser";

@Injectable({
    providedIn: 'root',
})

export class SessionVaultService implements OnDestroy {

    private Key?: CryptoKey;

    ngOnDestroy(): void {
        this.ClearUser();
    }

    public async CreateKey(password: string): Promise<void> {
        this.Key = await CreateCryptoKey(password);
    }

    public async DecryptUser(): Promise<ViewApplicationUser> {
        const user = await DecryptObject(sessionStorage.getItem('User')!, this.Key!) as ViewApplicationUser;
        return user;
    }

    public async EncryptUser(user: ViewApplicationUser): Promise<void> {
        sessionStorage.setItem('User', await EncryptObject(user, this.Key!));
    }

    public ClearUser() {
        this.Key = undefined;
        sessionStorage.removeItem('User');
    }
}