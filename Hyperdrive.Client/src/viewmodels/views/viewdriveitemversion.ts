import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';

export interface ViewDriveItemVersion extends ViewKey, ViewBase {
    Data?: string;
    Size: number;
    Type: string;
}
