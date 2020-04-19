import { ViewLink } from '../viewmodels/views/viewlink';

import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})

export class NavigationService {

    public GetManagementNavigationLinks(): ViewLink[] {
        return [
            {
                Label: 'Archives',
                Link: './management/archives',
                Index: 0,
                Class:'management-menu-option-image management-menu-archives-image'
            }, {
                Label: 'Shared Archives',
                Link: './management/sharedarchives',
                Index: 1,
                Class:'management-menu-option-image management-menu-shared-archives-image'
            }
        ];
    }

    public GetHomeNavigationLinks(): ViewLink[] {
        return [
            {
                Label: 'Managenent',
                Link: './management',
                Index: 0,
                Class:'home-menu-option-image home-menu-management-image'
            }, {
                Label: 'Security',
                Link: './security',
                Index: 1,
                Class:'home-menu-option-image home-menu-security-image'
            }
        ];
    }
}
