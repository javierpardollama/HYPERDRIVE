import { FilterPageBase } from "./filterpagebase";

export interface FilterPageDriveItem extends FilterPageBase {
    ApplicationUserId?: number;
    ParentId?: number;
    ParentName?: string;
}