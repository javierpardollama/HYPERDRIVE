import { UpdateBase } from './updatebase';

export interface UpdateApplicationUser extends UpdateBase {
    FirstName: string;
    LastName: string;
    PhoneNumber: string;
    ApplicationRolesId: number[];
}
