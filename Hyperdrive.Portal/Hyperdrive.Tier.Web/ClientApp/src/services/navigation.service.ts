import { ViewTab } from './../viewmodels/views/viewtab';

import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})

export class NavigationService {

    public GetArchiveManagementNavigationTabs(): ViewTab[] {
        return [
            {
                Label: 'Archives',
                Link: './management/archives',
                Index: 0
            }, {
                Label: 'Shared Archives',
                Link: './management/sharedarchives',
                Index: 1
            }
        ];
    }
}
