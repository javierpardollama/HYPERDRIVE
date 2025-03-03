import { ViewBase } from './viewbase';

export interface ViewApplicationUserRefreshToken extends ViewBase {
    Name: string;
    LoginProvider: string;
    Value: string;
    ExpiresAt: Date;
}
