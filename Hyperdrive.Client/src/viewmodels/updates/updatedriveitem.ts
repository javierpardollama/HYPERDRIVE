import { UpdateBase } from './updatebase';

export interface UpdateDriveItem extends UpdateBase {
    Data?: string;
    Name: string;
    Folder: boolean;
    Locked: boolean;
    Size?: number;
    Type?: string;
}
