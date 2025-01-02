import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';
import { ViewDriveItem } from './viewdriveitem';

export interface ViewDriveItemVersion extends ViewKey, ViewBase {
    DriveItem: ViewDriveItem;
    Name: string;
    Data?: string;
    Size: number;
    Type: string;
}
