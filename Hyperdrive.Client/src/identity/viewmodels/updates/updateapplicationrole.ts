import { UpdateBase } from './updatebase';

export interface UpdateApplicationRole extends UpdateBase {
    Name: string;
    ImageUri: string;
}
