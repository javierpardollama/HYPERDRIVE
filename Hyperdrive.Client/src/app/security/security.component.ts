import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';

import { ChangeNameModalComponent } from './modals/changename-modal/changename-modal.component';

import { ChangeEmailModalComponent } from './modals/changeemail-modal/changeemail-modal.component';

import { ChangePasswordModalComponent } from './modals/changepassword-modal/changepassword-modal.component';

import { ChangePhoneNumberModalComponent } from './modals/changephonenumber-modal/changephonenumber-modal.component';

import { ViewApplicationUser } from '../../viewmodels/views/viewapplicationuser';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { CommonModule } from '@angular/common';
import { SessionVaultService } from 'src/services/session.vault.service';


@Component({
    selector: 'app-security',
    templateUrl: './security.component.html',
    styleUrls: ['./security.component.scss'],
    imports:
        [
            MatExpansionModule,
            MatButtonModule,
            MatTooltipModule,
            MatDialogModule,
            CommonModule
        ]
})
export class SecurityComponent implements OnInit {

    public User?: ViewApplicationUser;

    // Constructor
    constructor(
        private sessionVaultService: SessionVaultService,
        public matDialog: MatDialog
    ) {

    }

    // Life Cicle
    async ngOnInit(): Promise<void> {
        await this.GetLocalUser()
    }

    ChangeName(): void {
        const dialogRef = this.matDialog.open(ChangeNameModalComponent, {
            width: '450px'
        });

        dialogRef.afterClosed().subscribe(() => {
            this.GetLocalUser();
        });
    }

    ChangeEmail(): void {
        const dialogRef = this.matDialog.open(ChangeEmailModalComponent, {
            width: '450px'
        });

        dialogRef.afterClosed().subscribe(() => {
            this.GetLocalUser();
        });
    }

    ChangePassword(): void {
        const dialogRef = this.matDialog.open(ChangePasswordModalComponent, {
            width: '450px'
        });

        dialogRef.afterClosed().subscribe(() => {
            this.GetLocalUser();
        });
    }

    ChangePhoneNumber(): void {
        const dialogRef = this.matDialog.open(ChangePhoneNumberModalComponent, {
            width: '450px'
        });

        dialogRef.afterClosed().subscribe(() => {
            this.GetLocalUser();
        });
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.sessionVaultService.DecryptUser();;
    }
}
