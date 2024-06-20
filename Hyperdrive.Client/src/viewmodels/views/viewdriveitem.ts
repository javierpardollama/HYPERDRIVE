import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';
import { ViewApplicationUserDriveItem } from './viewapplicationuserdriveitem';
import { ViewDriveItemVersion } from './viewdriveitemversion';
import { ViewApplicationUser } from './viewapplicationuser';

export interface ViewDriveItem extends ViewKey, ViewBase {
    By: ViewApplicationUser;   
    Folder: boolean;
    Locked: boolean;   
    DriveItemVersions: ViewDriveItemVersion[];
    ApplicationUserDriveItems: ViewApplicationUserDriveItem[];
    LastDriveItemVersion: ViewDriveItemVersion;
}
