import { ViewKey } from './viewkey';
import { ViewBase } from './viewbase';

export interface ViewApplicationRole extends ViewKey, ViewBase {
  Name: string;
  ImageUri: string;
}
