const algorithm = 'AES-CBC';
const keyLength = 256; // bits
const ivLength = 16;   // bytes

function Base64ToBytes(base64: string) {
    return Uint8Array.from(atob(base64), c => c.charCodeAt(0));
}

export async function Encrypt(object: Record<string, any>): Promise<string> {

    const iv = crypto.getRandomValues(new Uint8Array(ivLength));
    const keyData = crypto.getRandomValues(new Uint8Array(keyLength / 8));

    const key = await crypto.subtle.importKey(
        'raw',
        keyData,
        { name: algorithm },
        false, // allow export
        ['encrypt']
    );

    const encoded = new TextEncoder().encode(JSON.stringify(object));
    const encryptedBuffer = await crypto.subtle.encrypt(
        { name: algorithm, iv },
        key,
        encoded
    );

    const exportedKey = await crypto.subtle.exportKey('raw', key);
    const keyBase64 = btoa(String.fromCharCode(...new Uint8Array(exportedKey)));

    const encrypted = btoa(String.fromCharCode(...new Uint8Array(encryptedBuffer)));

    const data = {
        Key: keyBase64,
        Iv: btoa(String.fromCharCode(...iv)),
        Data: encrypted
    };

    return JSON.stringify(data);
}


export async function Decrypt(jsonstring: string): Promise<any> {

    const data = JSON.parse(jsonstring);

    const keyBytes = Base64ToBytes(data.Key);
    const iv = Base64ToBytes(data.Iv);
    const encryptedBytes = Base64ToBytes(data.Data);

    // Import the key
    const key = await crypto.subtle.importKey(
        'raw',
        keyBytes,
        { name: algorithm },
        false,
        ['decrypt']
    );

    // Decrypt
    const decryptedBuffer = await crypto.subtle.decrypt(
        { name: algorithm, iv },
        key,
        encryptedBytes
    );

    const decryptedText = new TextDecoder().decode(decryptedBuffer);
    return JSON.parse(decryptedText);
}

