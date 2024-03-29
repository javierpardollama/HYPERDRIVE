import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';
import { ViewApplicationUserArchive } from './viewapplicationuserarchive';
import { ViewArchiveVersion } from './viewarchiveversion';
import { ViewApplicationUser } from './viewapplicationuser';

export interface ViewArchive extends ViewKey, ViewBase {
    By: ViewApplicationUser;
    Name: string;
    Folder: boolean;
    Locked: boolean;
    System: boolean;
    ArchiveVersions: ViewArchiveVersion[];
    ApplicationUserArchives: ViewApplicationUserArchive[];
}
