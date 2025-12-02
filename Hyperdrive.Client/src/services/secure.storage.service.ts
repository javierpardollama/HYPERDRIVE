import { Injectable } from "@angular/core";
import { DeriveKey } from "src/utils/crypto.utils";
import { IsEmpty } from "src/utils/object.utils";

@Injectable({
    providedIn: 'root',
})
export class SecureStorageService {

    private CryptoKey?: CryptoKey;

    public async CreateKey(password: string): Promise<void> {
        const salt = crypto.getRandomValues(new Uint8Array(16));
        this.CryptoKey = await DeriveKey(password, salt);
    }

    public async StoreObject(key: string, value: any): Promise<void> {
        const iv = crypto.getRandomValues(new Uint8Array(12));
        const encoded = new TextEncoder().encode(JSON.stringify(value));
        const cipher = await crypto.subtle.encrypt({ name: "AES-GCM", iv }, this.CryptoKey!, encoded);

        const encryptedData = {
            iv: Array.from(iv),
            data: Array.from(new Uint8Array(cipher))
        };

        sessionStorage.setItem(key, JSON.stringify(encryptedData));
    }

    public async RetrieveObject<T>(key: string): Promise<T | undefined> {
        if (IsEmpty(this.CryptoKey)) return undefined;

        const encrypted = sessionStorage.getItem(key);

        if (IsEmpty(encrypted)) return undefined;

        const parsed = JSON.parse(encrypted!);
        const iv = new Uint8Array(parsed.iv);
        const data = new Uint8Array(parsed.data);

        const decrypted = await crypto.subtle.decrypt({ name: "AES-GCM", iv }, this.CryptoKey!, data);
        return JSON.parse(new TextDecoder().decode(decrypted)) as T;
    }

    public RemoveObject(key: string): void {
        sessionStorage.removeItem(key);
    }
}
