import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';
import { ViewDriveItem } from './viewdriveitem';
import { ViewApplicationUser } from './viewapplicationuser';

export interface ViewApplicationUserDriveItem extends ViewKey, ViewBase {
    DriveItem: ViewDriveItem;
    ApplicationUser: ViewApplicationUser;
}
