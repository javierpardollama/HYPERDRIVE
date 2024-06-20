import { UpdateBase } from './updatebase';

export interface UpdateDriveItem extends UpdateBase {
    Data?: ArrayBuffer;
    Name: string;
    Folder: boolean;
    Locked: boolean;
    Size?: number;
    Type?: string;
}
