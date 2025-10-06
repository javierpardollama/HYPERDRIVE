import {Component, OnInit} from '@angular/core';

import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

import {MatDialogRef} from '@angular/material/dialog';
import {MatSnackBar} from '@angular/material/snack-bar';

import {SecurityService} from '../../../../services/security.service';

import {SecurityEmailChange} from '../../../../viewmodels/security/securityemailchange';

import {ViewApplicationUser} from '../../../../viewmodels/views/viewapplicationuser';

import {TextAppVariants} from '../../../../variants/text.app.variants';

import {TimeAppVariants} from '../../../../variants/time.app.variants';

@Component({
    selector: 'app-changeemail-modal',
    templateUrl: './changeemail-modal.component.html',
    styleUrls: ['./changeemail-modal.component.scss']
})
export class ChangeEmailModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public User!: ViewApplicationUser;

    // Constructor
    constructor(
        public dialogRef: MatDialogRef<ChangeEmailModalComponent>,
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
        this.User = JSON.parse(sessionStorage.getItem('User')!);
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
                {duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks});

            sessionStorage.setItem('User', JSON.stringify(user));
        }

        this.dialogRef.close();
    }
}
