import { ViewApplicationUser } from '../views/viewapplicationuser';
import { UpdateBase } from './updatebase';

export interface UpdateArchive extends UpdateBase {
    By: ViewApplicationUser;
    Data: ArrayBuffer | string;
    Name: string;
    Size: number;
    Type: string;
}
