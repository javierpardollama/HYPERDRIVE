import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';
import { ProfileContextMenuComponent } from '../profile-context-menu/profile-context-menu.component';
import { ToolboxContextMenuComponent } from '../toolbox-context-menu/toolbox-context-menu.component';
import { MatBottomSheet } from "@angular/material/bottom-sheet";
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatBottomSheetModule } from "@angular/material/bottom-sheet";
import { SecureStorageService } from 'src/services/secure.storage.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-nav-menu',
    templateUrl: './nav-menu.component.html',
    styleUrls: ['./nav-menu.component.scss'],
    imports: [
        MatToolbarModule,
        MatButtonModule,
        MatSidenavModule,
        MatTooltipModule,
        MatBottomSheetModule
    ]
})
export class NavMenuComponent implements OnInit {
    // DI
    private secureStorageService = inject(SecureStorageService);
    bottomSheet = inject(MatBottomSheet);

    public User?: ViewApplicationUser;

    // Constructor
    constructor() {

    }

    // Life Cicle
    async ngOnInit(): Promise<void> {
        await this.GetLocalUser();
    }

    // Nav Actions
    public Profile(): void {
        const dialogRef = this.bottomSheet.open(ProfileContextMenuComponent, {});

        dialogRef.afterDismissed().subscribe(() => {
            this.GetLocalUser();
        });
    }

    public Toolbox(): void {
        const dialogRef = this.bottomSheet.open(ToolboxContextMenuComponent, {});

        dialogRef.afterDismissed().subscribe(() => {
            this.GetLocalUser();
        });
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);;
    }
}
