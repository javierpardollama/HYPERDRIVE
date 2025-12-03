import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { SecurityService } from '../../../../services/security.service';

import { SecurityPasswordChange } from '../../../../viewmodels/security/securitypasswordchange';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from '../../../../variants/text.app.variants';
import { TimeAppVariants } from '../../../../variants/time.app.variants';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SecureStorageService } from 'src/services/secure.storage.service';

@Component({
    selector: 'app-changepassword-modal',
    templateUrl: './changepassword-modal.component.html',
    styleUrls: ['./changepassword-modal.component.scss'],
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
export class ChangePasswordModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public User?: ViewApplicationUser;

    // Constructor
    constructor(
        public dialogRef: MatDialogRef<ChangePasswordModalComponent>,
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
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>('User');
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id,
                [Validators.required]),
            CurrentPassword: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [Validators.required]),
            NewPassword: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [Validators.required]),
        });
    }

    // Form Actions
    async onSubmit(viewModel: SecurityPasswordChange): Promise<void> {
        let user = await this.securityService.ChangePassword(viewModel);

        if (user) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

            await this.secureStorageService.CreateKey(viewModel.NewPassword);
            await this.secureStorageService.StoreObject('User', user);
        }

        this.dialogRef.close();
    }
}
