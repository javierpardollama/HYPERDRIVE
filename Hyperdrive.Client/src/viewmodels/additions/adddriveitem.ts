import { AddBase } from "./addbase";

export interface AddDriveItem extends AddBase {
    Data?: string;
    FileName: string;
    Folder: boolean;
    Size?: number;
    Type?: string;
    ApplicationUsersId: number[];
}
