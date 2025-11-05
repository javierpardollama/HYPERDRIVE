import { Component, OnInit } from '@angular/core';

import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { SecurityService } from '../../../../services/security.service';

import { SecurityPhoneNumberChange } from '../../../../viewmodels/security/securityphonenumberchange';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from '../../../../variants/text.app.variants';

import { TimeAppVariants } from '../../../../variants/time.app.variants';

import { ExpressionAppVariants } from '../../../../variants/expression.app.variants';
import { Decrypt, Encrypt } from 'src/services/crypto.sevice';

@Component({
    selector: 'app-changephonenumber-modal',
    templateUrl: './changephonenumber-modal.component.html',
    styleUrls: ['./changephonenumber-modal.component.scss']
})
export class ChangePhoneNumberModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public User!: ViewApplicationUser;

    // Constructor
    constructor(
        public dialogRef: MatDialogRef<ChangePhoneNumberModalComponent>,
        private securityService: SecurityService,
        private formBuilder: FormBuilder,
        private matSnackBar: MatSnackBar) {
    }

    // Life Cicle
    ngOnInit() {
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

            sessionStorage.setItem('User', Encrypt(user));
        }

        this.dialogRef.close();
    }
}
