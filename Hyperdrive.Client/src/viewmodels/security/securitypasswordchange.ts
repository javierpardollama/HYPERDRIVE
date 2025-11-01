import { SecurityBase } from "./securitybase";

export interface SecurityPasswordChange extends SecurityBase {
  CurrentPassword: string;
  NewPassword: string;
  ApplicationUserRefreshToken: string;
}
