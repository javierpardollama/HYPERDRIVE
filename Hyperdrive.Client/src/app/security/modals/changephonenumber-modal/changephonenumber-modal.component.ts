import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { SecurityService } from '../../../../services/security.service';

import { SecurityPhoneNumberChange } from '../../../../viewmodels/security/securityphonenumberchange';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from '../../../../variants/text.app.variants';

import { TimeAppVariants } from '../../../../variants/time.app.variants';

import { ExpressionAppVariants } from '../../../../variants/expression.app.variants';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SecureStorageService } from 'src/services/secure.storage.service';

@Component({
    selector: 'app-changephonenumber-modal',
    templateUrl: './changephonenumber-modal.component.html',
    styleUrls: ['./changephonenumber-modal.component.scss'],
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
export class ChangePhoneNumberModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public User?: ViewApplicationUser;

    // Constructor
    constructor(
        public dialogRef: MatDialogRef<ChangePhoneNumberModalComponent>,
        private securityService: SecurityService,
        private secureStorageService: SecureStorageService,
        private formBuilder: FormBuilder,
        private matSnackBar: MatSnackBar) {
    }

    // Life Cicle
    async ngOnInit(): Promise<void> {
        this.GetLocalUser();
        this.CreateForm();
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>('User');
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id,
                [Validators.required]),
            NewPhoneNumber: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [
                    Validators.required,
                    Validators.pattern(new RegExp(ExpressionAppVariants.AppPhoneNumberExpression))
                ]),
        });
    }

    // Form Actions
    async onSubmit(viewModel: SecurityPhoneNumberChange): Promise<void> {
        let user = await this.securityService.ChangePhoneNumber(viewModel);

        if (user) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

            await this.secureStorageService.StoreObject('User', user);
        }

        this.dialogRef.close();
    }
}
