import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';

export interface ViewDriveItemVersion extends ViewKey, ViewBase {
    Size?: number;
    Type: string;
}
