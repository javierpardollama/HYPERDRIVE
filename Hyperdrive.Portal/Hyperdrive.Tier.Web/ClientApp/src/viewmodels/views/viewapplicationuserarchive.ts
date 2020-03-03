import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';
import { ViewArchive } from './viewarchive';
import { ViewApplicationUser } from './viewapplicationuser';

export interface ViewApplicationUserArchive extends ViewKey, ViewBase {
    Archive: ViewArchive;
    ApplicationUser: ViewApplicationUser;
}
