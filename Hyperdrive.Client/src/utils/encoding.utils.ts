export function Base64StringToBytes(base64: string) {
    return Uint8Array.from(atob(base64), c => c.charCodeAt(0));
}