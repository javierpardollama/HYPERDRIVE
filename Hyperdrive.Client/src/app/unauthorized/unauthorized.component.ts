import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Router, RouterModule } from '@angular/router';

@Component({
    selector: 'app-unauthorized',
    templateUrl: './unauthorized.component.html',
    styleUrl: './unauthorized.component.scss',
    imports: [
        MatButtonModule,
        MatTooltipModule,
        RouterModule
    ]
})
export class UnauthorizedComponent {

    // Constructor
    constructor(private router: Router) {
    }

    public async Back(): Promise<void> {
        await this.router.navigate([""]);
    }
}
