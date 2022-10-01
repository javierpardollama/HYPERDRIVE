import { ViewApplicationUser } from '../views/viewapplicationuser';

export interface AddArchive {
    By: ViewApplicationUser;
    Data: ArrayBuffer | string | null;
    Name: string;
    Folder: boolean;
    Locked: boolean;
    Size: number;
    Type: string;
}
