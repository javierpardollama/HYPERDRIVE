import { AddBase } from "./addbase";

export interface AddArchive extends AddBase {
    Data?: ArrayBuffer;
    Name: string;
    Folder: boolean;
    Locked: boolean;
    Size?: number;
    Type?: string;
}
