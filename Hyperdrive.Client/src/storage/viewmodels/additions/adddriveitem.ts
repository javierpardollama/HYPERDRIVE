import { AddBase } from "./addbase";

export interface AddDriveItem extends AddBase {
    ParentId?: string;
    Data?: string;
    FileName: string;
    Folder: boolean;
    Size?: number;
    Type?: string;
}
