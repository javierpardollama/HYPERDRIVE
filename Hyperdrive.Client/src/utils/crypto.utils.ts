const Algorithm = 'AES-CBC';
const KeyLength = 256; // bits
const IvLength = 16;   // bytes

function Base64ToBytes(base64: string) {
    return Uint8Array.from(atob(base64), c => c.charCodeAt(0));
}

export async function Encrypt(object: Record<string, any>): Promise<string> {

    const iv = crypto.getRandomValues(new Uint8Array(IvLength));
    const keydata = crypto.getRandomValues(new Uint8Array(KeyLength / 8));

    const key = await crypto.subtle.importKey(
        'raw',
        keydata,
        { name: Algorithm },
        false, // allow export
        ['encrypt']
    );

    const encoded = new TextEncoder().encode(JSON.stringify(object));
    
    const encryptedBuffer = await crypto.subtle.encrypt(
        { name: Algorithm, iv },
        key,
        encoded
    );

    const exportedkey = await crypto.subtle.exportKey('raw', key);

    const keybase64 = btoa(String.fromCharCode(...new Uint8Array(exportedkey)));

    const encrypted = btoa(String.fromCharCode(...new Uint8Array(encryptedBuffer)));

    const data = {
        Key: keybase64,
        Iv: btoa(String.fromCharCode(...iv)),
        Data: encrypted
    };

    return JSON.stringify(data);
}


export async function Decrypt(jsonstring: string): Promise<any> {

    const data = JSON.parse(jsonstring);

    const keybytes = Base64ToBytes(data.Key);
    const iv = Base64ToBytes(data.Iv);
    const encryptedbytes = Base64ToBytes(data.Data);

    // Import the key
    const key = await crypto.subtle.importKey(
        'raw',
        keybytes,
        { name: Algorithm },
        false,
        ['decrypt']
    );

    // Decrypt
    const decryptedbuffer = await crypto.subtle.decrypt(
        { name: Algorithm, iv },
        key,
        encryptedbytes
    );

    const decryptedtext = new TextDecoder().decode(decryptedbuffer);
    return JSON.parse(decryptedtext);
}

