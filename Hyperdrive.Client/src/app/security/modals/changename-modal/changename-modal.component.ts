import { Component } from '@angular/core';

import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { SecurityService } from '../../../../services/security.service';

import { SecurityNameChange } from '../../../../viewmodels/security/securitynamechange';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from '../../../../variants/text.app.variants';

import { TimeAppVariants } from '../../../../variants/time.app.variants';
import { Decrypt, Encrypt } from 'src/services/crypto.sevice';

@Component({
    selector: 'app-changename-modal',
    templateUrl: './changename-modal.component.html',
    styleUrl: './changename-modal.component.scss'
})
export class ChangeNameModalComponent {

    public formGroup!: FormGroup;

    public User!: ViewApplicationUser;

    // Constructor
    constructor(
        public dialogRef: MatDialogRef<ChangeNameModalComponent>,
        private securityService: SecurityService,
        private formBuilder: FormBuilder,
        private matSnackBar: MatSnackBar) {
    }

    // Life Cicle
    ngOnInit(): void {
        this.GetLocalUser();
        this.CreateForm();
    }

    // Get User from Storage
    public GetLocalUser(): void {
        this.User = Decrypt(sessionStorage.getItem('User')!) as ViewApplicationUser;
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            ApplicationUserId: new FormControl<number>(this.User.Id,
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

            sessionStorage.setItem('User', Encrypt(user));
        }

        this.dialogRef.close();
    }

}
