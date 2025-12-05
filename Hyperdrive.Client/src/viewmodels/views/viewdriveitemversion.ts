import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';

export interface ViewDriveItemVersion extends ViewKey, ViewBase {
    FileName: string;
    Size?: number;
    Type: string;
}
