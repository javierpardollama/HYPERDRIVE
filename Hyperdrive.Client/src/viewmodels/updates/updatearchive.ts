import { UpdateBase } from './updatebase';

export interface UpdateArchive extends UpdateBase {
    Data: ArrayBuffer | string | null;
    Name: string;
    Folder: boolean;
    Locked: boolean;
    Size: number;
    Type: string;
}
