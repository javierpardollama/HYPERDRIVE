import { Injectable } from '@angular/core';

import {
    Router
} from '@angular/router';

import { ViewApplicationUser } from './../viewmodels/views/viewapplicationuser';

@Injectable({
    providedIn: 'root'
})

export class SignInGuard {

    private User!: ViewApplicationUser;

    private Activated = false;

    constructor(private router: Router) { }

    canActivate() {

        this.GetLocalUser();

        if (!this.User) {
            this.router.navigateByUrl('auth/signin');
        } else {
            this.Activated = true;
        }

        return this.Activated;
    }

    // Get User from Storage
    public GetLocalUser() {
        this.User = JSON.parse(localStorage.getItem('User')!);
    }
}
