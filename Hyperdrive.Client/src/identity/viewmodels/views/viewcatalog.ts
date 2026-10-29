import { ViewKey } from "./viewkey";
import { ViewBase } from "./viewbase";

export interface ViewCatalog extends ViewKey, ViewBase {
    Name: string;
}