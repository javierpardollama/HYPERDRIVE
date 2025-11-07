import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';
import { ViewCatalog } from './viewcatalog';

export interface ViewDriveItem extends ViewKey, ViewBase {
    By: ViewCatalog;
    Folder: boolean;
    Name: string;
    FileName: string;
    Extension: string;
    Parent: ViewCatalog;
    SharedWith: ViewCatalog[];
}
