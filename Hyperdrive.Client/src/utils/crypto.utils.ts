import { IsEmpty } from "./object.utils";
import { EncodeBase64, DecodeBase64 } from "./byte.utils";

const ALGORITHM = 'AES-GCM';
const IV_LENGTH = 12;
const SALT_LENGTH = 16;

export async function CreateCryptoKey(password: string) {
    const salt = crypto.getRandomValues(new Uint8Array(SALT_LENGTH));
    return await DeriveKey(password, salt);
}

export async function EncryptObject(object: Record<string, any>, key: CryptoKey): Promise<string> {
    const iv = crypto.getRandomValues(new Uint8Array(IV_LENGTH));

    const encoded = new TextEncoder().encode(JSON.stringify(object));

    const encryptedbuffer = await crypto.subtle.encrypt(
        { name: ALGORITHM, iv },
        key,
        encoded
    );

    return JSON.stringify({
        Iv: EncodeBase64(iv),
        Data: EncodeBase64(new Uint8Array(encryptedbuffer))
    });
}

export async function DecryptObject(jsonstring: string, cryptokey: CryptoKey): Promise<any> {
    if (IsEmpty(jsonstring) || IsEmpty(cryptokey)) return;

    const { Iv, Data } = JSON.parse(jsonstring);

    const iv = DecodeBase64(Iv);
    const encryptedbytes = DecodeBase64(Data);

    const decryptedbuffer = await crypto.subtle.decrypt(
        {
            name: ALGORITHM,
            iv: iv.buffer as ArrayBuffer
        },
        cryptokey,
        encryptedbytes.buffer as ArrayBuffer
    );

    return JSON.parse(new TextDecoder().decode(decryptedbuffer));
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
