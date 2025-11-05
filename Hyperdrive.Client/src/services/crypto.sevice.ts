import { CryptoData } from 'src/viewmodels/crypto/cryptodata';

const algorithm = 'AES-CBC';
const keyLength = 256; // bits
const ivLength = 16;   // bytes


export async function Encrypt(obj: Record<string, any>): Promise<string> {
  const json = JSON.stringify(obj);
  const iv = crypto.getRandomValues(new Uint8Array(ivLength));
  const keyData = crypto.getRandomValues(new Uint8Array(keyLength / 8));

  const key = await crypto.subtle.importKey(
    'raw',
    keyData,
    { name: algorithm },
    false,
    ['encrypt']
  );

  const encoded = new TextEncoder().encode(json);
  const encryptedBuffer = await crypto.subtle.encrypt(
    { name: algorithm, iv },
    key,
    encoded
  );

  const encrypted = btoa(String.fromCharCode(...new Uint8Array(encryptedBuffer)));

  const data: CryptoData = {
    Key: key,
    Iv: btoa(String.fromCharCode(...iv)),
    Data: encrypted
  };

  return JSON.stringify(data);
}

export async function Decrypt(encryptedStr: string): Promise<Record<string, any>> {
  const { Key, Iv, Data } = JSON.parse(encryptedStr) as CryptoData;

  const iv = Uint8Array.from(atob(Iv), c => c.charCodeAt(0));
  const encryptedBytes = Uint8Array.from(atob(Data), c => c.charCodeAt(0));

  const decryptedBuffer = await crypto.subtle.decrypt(
    { name: algorithm, iv },
    Key,
    encryptedBytes
  );

  const decrypted = new TextDecoder().decode(decryptedBuffer);
  return JSON.parse(decrypted);
}
