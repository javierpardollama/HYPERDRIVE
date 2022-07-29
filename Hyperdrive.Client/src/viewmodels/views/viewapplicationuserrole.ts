import { ViewBase } from './viewbase';
import { ViewApplicationRole } from './viewapplicationrole';
import { ViewApplicationUser } from './viewapplicationuser';

export interface ViewApplicationUserRole extends ViewBase {
    ApplicationRole: ViewApplicationRole;
    ApplicationUser: ViewApplicationUser;
}
