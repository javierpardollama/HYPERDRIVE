import { GetBytes } from "./crypto.utils";

export async function EncodeBlob(file: File): Promise<string> {

    const bytes = new Uint8Array(await file.arrayBuffer());

    const binarystring = Array.from(bytes).map(byte => String.fromCharCode(byte)).join('');

    return window.btoa(binarystring);
}

export async function DecodeBlob(content: string, type: string): Promise<Blob> {

    const bytes = GetBytes(content);

    return new Blob([bytes], { type: type });
}