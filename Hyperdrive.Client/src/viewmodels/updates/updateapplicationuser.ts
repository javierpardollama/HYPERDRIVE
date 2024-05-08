import { UpdateBase } from './updatebase';

export interface UpdateApplicationUser extends UpdateBase {
    FirstName: string;
    LastName: string;   
    ApplicationRolesId: number[];
}
