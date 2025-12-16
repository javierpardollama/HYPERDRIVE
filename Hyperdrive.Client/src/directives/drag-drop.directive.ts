import { Directive, HostListener, Output, EventEmitter, HostBinding } from '@angular/core';

@Directive({
    selector: '[app-drag-drop]'
})
export class DragDropDirective {

    private InProgress: boolean = false;

    @HostBinding('class.dragging') get dragInProgress() {
        return this.InProgress;
    }

    @Output() OnDropped: EventEmitter<any>;

    constructor() {
        this.OnDropped = new EventEmitter();
    }

    @HostListener('dragenter', ['$event'])
    @HostListener('dragover', ['$event'])
    OnStart(event: DragEvent): void {
        event.dataTransfer!.dropEffect = 'move';
        this.StopDefault(event);
        this.InProgress = true;
    }

    @HostListener('dragleave', ['$event'])
    @HostListener('dragend', ['$event'])
    OnStop(event: DragEvent): void {
        this.StopDefault(event);
        event.dataTransfer!.effectAllowed = 'copy';
        this.InProgress = false;
    }

    @HostListener('drop', ['$event'])
    OnDrop(event: UIEvent): void {
        this.StopDefault(event);
        this.InProgress = false;
        this.OnDropped.emit(event);
    }

    private StopDefault(e: UIEvent): void {
        e.stopPropagation();
        e.preventDefault();
    }

}
