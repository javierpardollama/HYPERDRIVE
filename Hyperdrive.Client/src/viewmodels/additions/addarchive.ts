export interface AddArchive {
    ApplicationUserId: number;
    Data: ArrayBuffer | string | null;
    Name: string;
    Folder: boolean;
    Locked: boolean;
    Size: number;
    Type: string;
}
