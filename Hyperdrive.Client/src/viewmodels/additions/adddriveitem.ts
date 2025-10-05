import { AddBase } from "./addbase";

export interface AddDriveItem extends AddBase {
    ParentId?:number;
    Data?: string;
    FileName: string;
    Folder: boolean;
    Size?: number;
    Type?: string;
}
