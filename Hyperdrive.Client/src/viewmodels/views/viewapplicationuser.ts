import { ViewKey } from './viewkey';
import { ViewBase } from './viewbase';
import { ViewApplicationUserRole } from './viewapplicationuserrole';
import { ViewApplicationRole } from './viewapplicationrole';
import { ViewApplicationUserToken } from './viewapplicationusertoken';
import { ViewApplicationUserRefreshToken } from './viewapplicationuserrefreshtoken';

export interface ViewApplicationUser extends ViewKey, ViewBase {
  Email: string;
  FirstName: string;
  LastName: string;
  PhoneNumber: string;
  Initial: string;
  ApplicationUserRoles: ViewApplicationUserRole[];
  ApplicationRoles: ViewApplicationRole[];
  ApplicationUserTokens: ViewApplicationUserToken[];
  ApplicationUserToken: ViewApplicationUserToken;
  ApplicationUserRefreshTokens: ViewApplicationUserRefreshToken[];
  ApplicationUserRefreshToken: ViewApplicationUserRefreshToken;
}
