import { AddBase } from "./addbase";

export interface AddDriveItem extends AddBase {
    Data?: string;
    Name: string;
    Folder: boolean;
    Size?: number;
    Type?: string;
}
