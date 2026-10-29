export async function DeriveKey(
    password: string,
    salt: Uint8Array<ArrayBuffer>,
    options?: {
        iterations?: number;  // default tuneable
        hash?: 'SHA-256' | 'SHA-384' | 'SHA-512';
        extractable?: boolean; // default false for better security
        keyUsages?: KeyUsage[]; // default ['encrypt','decrypt']
    }
): Promise<CryptoKey> {
    const {
        iterations = 150_000,    // tune based on perf budget (100k–600k typical)
        hash = 'SHA-256',
        extractable = false,
        keyUsages = ['encrypt', 'decrypt'],
    } = options || {};

    if (!password) {
        throw new Error('Password must be a non-empty string.');
    }
    if (!(salt instanceof Uint8Array) || salt.length < 16) {
        throw new Error('Salt must be a Uint8Array of at least 16 bytes.');
    }

    const enc = new TextEncoder();
    const keyMaterial = await crypto.subtle.importKey(
        'raw',
        enc.encode(password),
        { name: 'PBKDF2' },
        false,
        ['deriveKey']
    );

    return crypto.subtle.deriveKey(
        {
            name: 'PBKDF2',
            salt,
            iterations,
            hash,
        },
        keyMaterial,
        { name: 'AES-GCM', length: 256 },
        extractable,
        keyUsages
    );
}
