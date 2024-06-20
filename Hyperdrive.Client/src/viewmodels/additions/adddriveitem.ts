import { AddBase } from "./addbase";

export interface AddDriveItem extends AddBase {
    Data?: ArrayBuffer;
    Name: string;
    Folder: boolean;
    Locked: boolean;
    Size?: number;
    Type?: string;
}
