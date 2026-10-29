export class ViewScroll {
    TableViewHeight: number = 0;
    TableScrollHeight: number = 0;
    ScrollLocation: number = 0;
    PageSize: number = 0;
    DataLength: number = 0;
    PageLength: number = 0;


    constructor(target: HTMLElement, pageSize: number, dataLength: number, pageLength: number) {
        this.TableViewHeight = target.offsetHeight;
        this.TableScrollHeight = target.scrollHeight;
        this.ScrollLocation = target.scrollTop;
        this.PageSize = pageSize;
        this.DataLength = dataLength;
        this.PageLength = pageLength;
    }

    IsReached(): boolean {
        return this.ScrollLocation > (this.TableScrollHeight - this.TableViewHeight - this.PageSize)
            && this.DataLength < this.PageLength;
    }
}