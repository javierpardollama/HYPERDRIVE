import { UpdateBase } from './updatebase';

export interface UpdateApplicationUser extends UpdateBase {
    ApplicationRoleNames: string[];
}
