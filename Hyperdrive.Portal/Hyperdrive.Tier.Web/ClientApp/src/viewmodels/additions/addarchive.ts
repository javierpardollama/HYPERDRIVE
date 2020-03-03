import { ViewApplicationUser } from '../views/viewapplicationuser';

export interface AddArchive {
    By: ViewApplicationUser;
    Data: ArrayBuffer | string;
    Name: string;
    Size: number;
    Type: string;
}
