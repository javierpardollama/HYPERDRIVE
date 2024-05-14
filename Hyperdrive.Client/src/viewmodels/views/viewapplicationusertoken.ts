import { ViewBase } from './viewbase';

export interface ViewApplicationUserToken extends ViewBase {
    Name: string;
    LoginProvider: string;
    Value: string;
}
