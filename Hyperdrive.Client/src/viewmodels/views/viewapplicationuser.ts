import { ViewKey } from './viewkey';
import { ViewBase } from './viewbase';
import {ViewCatalog} from './viewcatalog';
import {ViewToken} from './viewtoken';

export interface ViewApplicationUser extends ViewKey, ViewBase {
  Email: string;
  FirstName: string;
  LastName: string;
  PhoneNumber: string;
  Initial: string;
  ApplicationRoles: ViewCatalog[];
  Token?: ViewToken;
  RefreshToken?: ViewToken;
}
