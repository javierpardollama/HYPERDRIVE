import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { SecurityService } from '../../../../services/security.service';

import { SecurityEmailChange } from '../../../../viewmodels/security/securityemailchange';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from '../../../../variants/text.app.variants';

import { TimeAppVariants } from '../../../../variants/time.app.variants';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SessionVaultService } from 'src/services/session.vault.service';

@Component({
    selector: 'app-changeemail-modal',
    templateUrl: './changeemail-modal.component.html',
    styleUrls: ['./changeemail-modal.component.scss'],
    imports: [
        MatDialogModule,
        MatButtonModule,
        FormsModule,
        MatTooltipModule,
        MatSnackBarModule,
        MatInputModule,
        MatFormFieldModule,
        ReactiveFormsModule
    ]
})
export class ChangeEmailModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public User!: ViewApplicationUser;

    // Constructor
    constructor(
        public dialogRef: MatDialogRef<ChangeEmailModalComponent>,
        private securityService: SecurityService,
        private sessionVaultService: SessionVaultService,
        private formBuilder: FormBuilder,
        private matSnackBar: MatSnackBar) {
    }

    // Life Cicle
    async ngOnInit(): Promise<void> {
        await this.GetLocalUser();
        this.CreateForm();
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.sessionVaultService.DecryptUser();;
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            ApplicationUserId: new FormControl<number>(this.User.Id,
                [Validators.required]),
            NewEmail: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [
                    Validators.required,
                    Validators.email
                ]),
        });
    }

    // Form Actions
    async onSubmit(viewModel: SecurityEmailChange): Promise<void> {
        let user = await this.securityService.ChangeEmail(viewModel);

        if (user) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

            await this.sessionVaultService.EncryptUser(user);
        }

        this.dialogRef.close();
    }
}
