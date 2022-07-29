import { ViewKey } from './viewkey';
import { ViewBase } from './viewbase';
import { ViewApplicationUserRole } from './viewapplicationuserrole';
import { ViewApplicationRole } from './viewapplicationrole';
import { ViewApplicationUserToken } from './viewapplicationusertoken';
import { ViewApplicationUserArchive } from './viewapplicationuserarchive';

export interface ViewApplicationUser extends ViewKey, ViewBase {
  Email: string;
  Initial: string;
  ApplicationUserRoles: ViewApplicationUserRole[];
  ApplicationRoles: ViewApplicationRole[];
  ApplicationUserTokens: ViewApplicationUserToken[];
  ApplicationUserToken: ViewApplicationUserToken;
  ApplicationUserArchives: ViewApplicationUserArchive[];
}
