export interface AddArchive {
    ApplicationUserId: number;
    Data?: ArrayBuffer;
    Name: string;
    Folder: boolean;
    Locked: boolean;
    Size: number;
    Type: string;
}
