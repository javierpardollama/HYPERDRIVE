import { ViewLink } from '../viewmodels/views/viewlink';

import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})

export class NavigationService {
   

    public GetToolBoxNavigationLinks(): ViewLink[] {
        return [
            {
                Id:'nav-aplication-users',
                Label: 'Users',
                Link: './management/applicationuser',
                Index: 0,
                Class:'option-image application-user-management-image'
            },
            {
                Id:'nav-aplication-roles',
                Label: 'Roles',
                Link: './management/applicationrole',
                Index: 1,
                Class:'option-image application-role-management-image'
            },
            {
                Id:'nav-security',
                Label: 'Security',
                Link: './security',
                Index: 2,
                Class:'option-image security-image'
            }, {
                Id:'nav-app',
                Label: 'Hyperdrive',
                Link: './',
                Index: 3,
                Class:'option-image app-image'
            }
        ];
    }
}
