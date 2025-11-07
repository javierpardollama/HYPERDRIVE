import { ViewLink } from '../viewmodels/views/viewlink';

import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})

export class NavigationService {


    public GetToolBoxNavigationLinks(): ViewLink[] {
        return [
            {
                Id: 'nav-aplication-users',
                Label: 'Users',
                Link: './management/applicationuser',
                Index: 0,
                Class: 'sheet-option-image application-user-management-image'
            },
            {
                Id: 'nav-aplication-roles',
                Label: 'Roles',
                Link: './management/applicationrole',
                Index: 1,
                Class: 'sheet-option-image application-role-management-image'
            },
            {
                Id: 'nav-security',
                Label: 'Security',
                Link: './security',
                Index: 2,
                Class: 'sheet-option-image security-image'
            }, {
                Id: 'nav-drive',
                Label: 'Drive',
                Link: './',
                Index: 3,
                Class: 'sheet-option-image cloud-image'
            }
        ];
    }
}
