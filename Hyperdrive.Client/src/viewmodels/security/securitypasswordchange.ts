import { ViewApplicationUser } from './../views/viewapplicationuser';

export interface SecurityPasswordChange {
  CurrentPassword: string;
  NewPassword: string;
  ApplicationUser: ViewApplicationUser;
}
