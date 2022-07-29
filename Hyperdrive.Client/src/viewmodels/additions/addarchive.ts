import { ViewApplicationUser } from '../views/viewapplicationuser';

export interface AddArchive {
    By: ViewApplicationUser;
    Data: ArrayBuffer | string | null;
    Name: string;
    Size: number;
    Type: string;
}
