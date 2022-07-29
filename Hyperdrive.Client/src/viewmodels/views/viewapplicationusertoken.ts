import { ViewBase } from './viewbase';
import { ViewApplicationUser } from './viewapplicationuser';

export interface ViewApplicationUserToken extends ViewBase {
    Value: string;
    ApplicationUser: ViewApplicationUser;
}
