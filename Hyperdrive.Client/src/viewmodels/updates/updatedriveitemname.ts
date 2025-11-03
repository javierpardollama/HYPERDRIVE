import { UpdateBase } from './updatebase';

export interface UpdateDriveItemName extends UpdateBase {
    Name: string;
    Extension: string;
    ParentId?: number;
}
