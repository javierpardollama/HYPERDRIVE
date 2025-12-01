import { Injectable } from '@angular/core';

import {
    Router
} from '@angular/router';

import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';
import { SessionService } from 'src/services/session.service';

@Injectable({
    providedIn: 'root'
})

export class SignInGuard {

    private User?: ViewApplicationUser;

    private Activated = false;

    constructor(private sessionService: SessionService, private router: Router) { }

    async canActivate() {

        await this.GetLocalUser();

        if (!this.User) {
            this.router.navigateByUrl('auth/signin');
        } else {
            this.Activated = true;
        }

        return this.Activated;
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.sessionService.DecryptUser();
    }
}
