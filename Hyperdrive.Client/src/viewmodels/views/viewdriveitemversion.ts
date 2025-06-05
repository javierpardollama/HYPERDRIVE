import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';

export interface ViewDriveItemVersion extends ViewKey, ViewBase {
    Name: string;
    Data?: string;
    Size: number;
    Type: string;
}
