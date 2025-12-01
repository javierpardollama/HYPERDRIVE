export function EncodeBase64(bytes: Uint8Array): string {
    return btoa(String.fromCharCode(...bytes));
}

export function DecodeBase64(base64: string): Uint8Array<ArrayBuffer> {
    return Uint8Array.from(atob(base64), c => c.charCodeAt(0));
}
