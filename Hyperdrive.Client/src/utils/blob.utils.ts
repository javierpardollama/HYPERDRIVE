export async function FileToBase64(file: File): Promise<string> {

    const bytes = new Uint8Array(await file.arrayBuffer());

    const binaryString = Array.from(bytes).map(byte => String.fromCharCode(byte)).join('');

    return window.btoa(binaryString);
}

export async function Base64ToBlob(content: string, type: string): Promise<Blob> {

    const bytes = new Uint8Array(
        Array.from(window.atob(content!), char => char.charCodeAt(0))
    );

    return new Blob([bytes], { type: type });
}