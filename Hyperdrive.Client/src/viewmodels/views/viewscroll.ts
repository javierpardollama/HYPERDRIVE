export class ViewScroll {
    TableViewHeight: number = 0;
    TableScrollHeight: number = 0;
    ScrollLocation: number = 0;
    PageSize: number = 0;

    constructor(target: HTMLElement, pageSize: number) {
        this.TableViewHeight = target.offsetHeight;
        this.TableScrollHeight = target.scrollHeight;
        this.ScrollLocation = target.scrollTop;
        this.PageSize = pageSize;
    }

    IsReached(): boolean {
        return this.ScrollLocation > (this.TableScrollHeight - this.TableViewHeight - this.PageSize);
    }
}