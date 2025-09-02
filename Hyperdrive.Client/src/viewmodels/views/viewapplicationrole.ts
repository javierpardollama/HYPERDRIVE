import { ViewKey } from './viewkey';
import { ViewBase } from './viewbase';
import { ViewCatalog} from './viewcatalog';

export interface ViewApplicationRole extends ViewKey, ViewBase {
  Name: string;
  ImageUri: string;
  ApplicationUsers: ViewCatalog[];
}
