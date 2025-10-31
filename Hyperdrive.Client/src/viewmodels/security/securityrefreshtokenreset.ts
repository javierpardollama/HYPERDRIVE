import { SecurityBase } from "./securitybase";

export interface SecurityRefreshTokenReset extends SecurityBase {
  ApplicationUserRefreshToken: string;
}

