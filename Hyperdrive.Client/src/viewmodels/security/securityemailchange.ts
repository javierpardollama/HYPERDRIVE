import { SecurityBase } from "./securitybase";

export interface SecurityEmailChange extends SecurityBase {
  NewEmail: string;
  ApplicationUserRefreshToken: string;
}
