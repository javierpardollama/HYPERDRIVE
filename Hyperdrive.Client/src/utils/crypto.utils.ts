import { IsEmpty } from "./object.utils";

const ALGORITHM = 'AES-GCM'; // Use AES-GCM for confidentiality + integrity
const IV_LENGTH = 12;        // bytes (recommended for AES-GCM)
const SALT_LENGTH = 16;      // bytes for PBKDF2

export function GetBytes(base64: string) {
    return Uint8Array.from(atob(base64), c => c.charCodeAt(0));
}

export async function CreateCryptoKey(password: string) {
    const salt = crypto.getRandomValues(new Uint8Array(SALT_LENGTH));
    return await DeriveKey(password, salt);
}


export async function EncryptObject(object: Record<string, any>, key: CryptoKey): Promise<string> {
    const iv = crypto.getRandomValues(new Uint8Array(IV_LENGTH));

    const encoded = new TextEncoder().encode(JSON.stringify(object));

    const encryptedBuffer = await crypto.subtle.encrypt(
        { name: ALGORITHM, iv },
        key,
        encoded
    );

    return JSON.stringify({
        Iv: ToBase64(iv),
        Data: ToBase64(new Uint8Array(encryptedBuffer))
    });
}

export async function DecryptObject(jsonstring: string, cryptokey: CryptoKey): Promise<any> {
    if (IsEmpty(jsonstring) || IsEmpty(cryptokey)) return;

    const { Iv, Data } = JSON.parse(jsonstring);

    const iv = FromBase64(Iv);
    const encryptedBytes = FromBase64(Data);

    const decryptedBuffer = await crypto.subtle.decrypt(
        {
            name: ALGORITHM,
            iv: iv.buffer as ArrayBuffer
        },
        cryptokey,
        encryptedBytes.buffer as ArrayBuffer
    );

    return JSON.parse(new TextDecoder().decode(decryptedBuffer));
}

function ToBase64(bytes: Uint8Array): string {
    return btoa(String.fromCharCode(...bytes));
}

function FromBase64(base64: string): Uint8Array {
    return Uint8Array.from(atob(base64), c => c.charCodeAt(0));
}

async function DeriveKey(password: string, salt: Uint8Array): Promise<CryptoKey> {
    const encoder = new TextEncoder();
    const keyMaterial = await crypto.subtle.importKey(
        'raw',
        encoder.encode(password),
        { name: 'PBKDF2' },
        false,
        ['deriveKey']
    );

    return crypto.subtle.deriveKey(
        {
            name: 'PBKDF2',
            salt: salt.buffer as ArrayBuffer,
            iterations: 100000,
            hash: 'SHA-256'
        },
        keyMaterial,
        {
            name: ALGORITHM,
            length: 256
        },
        false,
        ['encrypt', 'decrypt']
    );
}
