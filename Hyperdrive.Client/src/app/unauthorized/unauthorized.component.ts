import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-unauthorized',
    templateUrl: './unauthorized.component.html',
    styleUrl: './unauthorized.component.scss',
    standalone: false
})
export class UnauthorizedComponent {

    // Constructor
    constructor(private router: Router) {
    }

    public async Back(): Promise<void> {
        await this.router.navigate([""]);
    }
}
