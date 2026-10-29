import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';
import { ViewCatalog } from './viewcatalog';

export interface ViewDriveItem extends ViewKey, ViewBase {
    ByExternalId: number;
    Folder: boolean;
    Name: string;
    FileName: string;
    Extension: string;
    Parent: ViewCatalog;
    SharedWith: number[];
    Downloadeable: boolean;
}
