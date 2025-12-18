import { Injectable } from "@angular/core";
import { CryptoMeta } from "src/viewmodels/crypto/crytpometa";
import { DecodeBase64, EncodeBase64 } from "src/utils/byte.utils";
import { DeriveKey } from "src/utils/crypto.utils";
import { VAULT_META_KEY } from "src/variants/vault.keys.variants";
import { CryptoData } from "src/viewmodels/crypto/cryptodata";

@Injectable({
    providedIn: 'root',
})
export class SecureStorageService {

    private CryptoKey?: CryptoKey;

    private Salt?: Uint8Array<ArrayBuffer>;

    public async CreateKey(password: string): Promise<void> {

        if (!password) throw new Error('Password must be non-empty');

        // Load or create vault metadata
        const metaraw = sessionStorage.getItem(VAULT_META_KEY);

        let meta: CryptoMeta

        if (metaraw) {
            meta = JSON.parse(metaraw) as CryptoMeta;
            if (meta.V !== 1) throw new Error('Unsupported vault version');
            this.Salt = DecodeBase64(meta.SaltBase64);
        } else {
            const salt = crypto.getRandomValues(new Uint8Array(16));
            meta = { V: 1, SaltBase64: EncodeBase64(salt) };
            sessionStorage.setItem(VAULT_META_KEY, JSON.stringify(meta));
            this.Salt = salt;
        }

        this.CryptoKey = await DeriveKey(password, this.Salt);
    }

    public async StoreObject(key: string, value: any): Promise<void> {
        const iv = crypto.getRandomValues(new Uint8Array(12));
        const encoded = new TextEncoder().encode(JSON.stringify(value));
        const cipher = await crypto.subtle.encrypt({ name: "AES-GCM", iv }, this.CryptoKey!, encoded);

        const encryptedData: CryptoData = {
            V: 1 as const,
            IvBase64: EncodeBase64(iv),
            DataBase64: EncodeBase64(cipher),
        };

        sessionStorage.setItem(key, JSON.stringify(encryptedData));
    }

    public async RetrieveObject<T>(key: string): Promise<T | undefined> {

        if (!this.CryptoKey) return undefined;
        const raw = sessionStorage.getItem(key);
        if (!raw) return undefined;

        let parsed: CryptoData;

        try {
            parsed = JSON.parse(raw) as CryptoData;
        } catch {
            // Tampered or legacy; remove
            sessionStorage.removeItem(key);
            return undefined;
        }

        if (parsed.V !== 1 || !parsed.IvBase64 || !parsed.DataBase64) {
            sessionStorage.removeItem(key);
            return undefined;
        }

        const iv = DecodeBase64(parsed.IvBase64);
        const data = DecodeBase64(parsed.DataBase64);

        try {
            const decrypted = await crypto.subtle.decrypt({ name: 'AES-GCM', iv }, this.CryptoKey, data);
            return JSON.parse(new TextDecoder().decode(decrypted)) as T;
        } catch {
            // Auth tag mismatch (tampering), wrong password/salt, or corrupted data
            return undefined;
        }
    }

    public RemoveObject(key: string): void {
        sessionStorage.removeItem(key);
    }
}   
