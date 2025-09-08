import { UpdateBase } from "./updatebase";

export interface UpdateDriveItem extends UpdateBase {
    Data?: string;
    FileName: string;
    Folder: boolean;
    Size?: number;
    Type?: string;
}
