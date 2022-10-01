import { ViewApplicationUser } from '../views/viewapplicationuser';
import { UpdateBase } from './updatebase';

export interface UpdateArchive extends UpdateBase {
    By: ViewApplicationUser;
    Data: ArrayBuffer | string | null;
    Name: string;
    Folder: boolean;
    Locked: boolean;
    Size: number;
    Type: string;
}
