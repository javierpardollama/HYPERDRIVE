import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';
import {ViewCatalog} from './viewcatalog';

export interface ViewDriveItem extends ViewKey, ViewBase {
    By: ViewCatalog;
    Folder: boolean;
    System: boolean;
    Name: string;
    Parent: ViewCatalog;
    SharedWith: ViewCatalog[];
}
