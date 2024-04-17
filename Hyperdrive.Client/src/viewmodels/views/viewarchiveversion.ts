import { ViewBase } from './viewbase';
import { ViewKey } from './viewkey';
import { ViewArchive } from './viewarchive';

export interface ViewArchiveVersion extends ViewKey, ViewBase {
    Archive: ViewArchive;
    Data?: ArrayBuffer; 
    Size: number;
    Type: string;
}
