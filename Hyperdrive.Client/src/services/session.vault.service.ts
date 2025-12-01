import { Injectable } from "@angular/core";
import { CreateCryptoKey, DecryptObject, EncryptObject } from "src/utils/crypto.utils";
import { ViewApplicationUser } from "src/viewmodels/views/viewapplicationuser";

@Injectable({
    providedIn: 'root',
})

export class SessionVaultService {

    private Key?: CryptoKey;

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
}