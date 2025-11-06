import { Base64StringToBytes } from "./encoding.utils";

export async function FileToBase64String(file: File): Promise<string> {

    const bytes = new Uint8Array(await file.arrayBuffer());

    const binaryString = Array.from(bytes).map(byte => String.fromCharCode(byte)).join('');

    return window.btoa(binaryString);
}

export async function Base64FileStringToBlob(content: string, type: string): Promise<Blob> {

    const bytes = Base64StringToBytes(content);

    return new Blob([bytes], { type: type });
}