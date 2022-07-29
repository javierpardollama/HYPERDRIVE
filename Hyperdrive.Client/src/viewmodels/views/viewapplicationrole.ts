import { ViewKey } from './viewkey';
import { ViewBase } from './viewbase';
import { ViewApplicationUserRole } from './viewapplicationuserrole';
import { ViewApplicationUser } from './viewapplicationuser';

export interface ViewApplicationRole extends ViewKey, ViewBase {
  Name: string;
  ImageUri: string;
  ApplicationUserRoles: ViewApplicationUserRole[];
  ApplicationUsers: ViewApplicationUser[];
}
