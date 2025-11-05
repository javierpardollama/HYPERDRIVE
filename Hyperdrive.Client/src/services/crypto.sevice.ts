import * as crypto from 'crypto';
import { CryptoData } from 'src/viewmodels/crypto/cryptodata';

const algorithm = 'aes-256-cbc';
const key = crypto.randomBytes(32); // 256-bit key
const iv = crypto.randomBytes(16);  // 128-bit IV

export function Encrypt(obj: Record<string, any>): string {
  const json = JSON.stringify(obj);
  const cipher = crypto.createCipheriv(algorithm, key, iv);
  let encrypted = cipher.update(json, 'utf8', 'base64');
  encrypted += cipher.final('base64');
  let data: CryptoData = {
    Iv: iv.toString('base64'),
    Data: encrypted
  }
  return JSON.stringify(data);
}

export function Decrypt(encryptedStr: string): Record<string, any> {
  const { Iv, Data } = JSON.parse(encryptedStr) as CryptoData;
  const decipher = crypto.createDecipheriv(algorithm, key, Buffer.from(Iv, 'base64'));
  let decrypted = decipher.update(Data, 'base64', 'utf8');
  decrypted += decipher.final('utf8');
  return JSON.parse(decrypted);
}
