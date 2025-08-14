import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';
import { ViewApplicationUserDriveItem } from './viewapplicationuserdriveitem';
import { ViewApplicationUser } from './viewapplicationuser';

export interface ViewDriveItem extends ViewKey, ViewBase {
    By: ViewApplicationUser;   
    Folder: boolean;
    Locked: boolean;
    Name: string;
    ApplicationUserDriveItems: ViewApplicationUserDriveItem[];
}
