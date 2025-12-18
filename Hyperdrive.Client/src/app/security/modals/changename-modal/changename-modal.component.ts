import { Component } from '@angular/core';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { SecurityService } from '../../../../services/security.service';

import { SecurityNameChange } from '../../../../viewmodels/security/securitynamechange';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from '../../../../variants/text.app.variants';

import { TimeAppVariants } from '../../../../variants/time.app.variants';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';

@Component({
    selector: 'app-changename-modal',
    templateUrl: './changename-modal.component.html',
    styleUrl: './changename-modal.component.scss',
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
export class ChangeNameModalComponent {

    public formGroup!: FormGroup;

    public User?: ViewApplicationUser;

    // Constructor
    constructor(
        public dialogRef: MatDialogRef<ChangeNameModalComponent>,
        private securityService: SecurityService,
        private secureStorageService: SecureStorageService,
        private formBuilder: FormBuilder,
        private matSnackBar: MatSnackBar) {
    }

    // Life Cicle
    async ngOnInit(): Promise<void> {
        this.CreateForm();
        await this.GetLocalUser();
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id,
                [Validators.required]),
            NewFirstName: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [
                    Validators.required,
                ]),
            NewLastName: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [
                    Validators.required,
                ]),
        });
    }

    // Form Actions
    async onSubmit(viewModel: SecurityNameChange): Promise<void> {
        let user = await this.securityService.ChangeName(viewModel);

        if (user) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

            await this.secureStorageService.StoreObject(VaultKeyAppVariants.VAULT_USER_KEY, user);
        }

        this.dialogRef.close();
    }

}
