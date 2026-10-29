export interface ViewToken {
    IssuedAt: Date;
    ExpiresAt: Date;
    LoginProvider: string;
    Value: string;
}