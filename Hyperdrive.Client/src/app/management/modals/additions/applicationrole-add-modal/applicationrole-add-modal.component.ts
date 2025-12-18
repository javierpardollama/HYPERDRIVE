import { Component, OnInit } from '@angular/core';

import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { AddApplicationRole } from '../../../../../viewmodels/additions/addapplicationrole';

import { ApplicationRoleService } from '../../../../../services/applicationrole.service';

import { TextAppVariants } from '../../../../../variants/text.app.variants';

import { TimeAppVariants } from '../../../../../variants/time.app.variants';

import { ExpressionAppVariants } from '../../../../../variants/expression.app.variants';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ViewApplicationUser } from 'src/viewmodels/views/viewapplicationuser';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { VAULT_USER_KEY } from 'src/variants/vault.keys.variants';

@Component({
    selector: 'app-applicationrole-add-modal',
    templateUrl: './applicationrole-add-modal.component.html',
    styleUrls: ['./applicationrole-add-modal.component.scss'],
    imports: [
        MatSnackBarModule,
        MatDialogModule,
        FormsModule,
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        ReactiveFormsModule
    ]
})
export class ApplicationRoleAddModalComponent implements OnInit {

    public User?: ViewApplicationUser;

    public formGroup!: FormGroup;

    // Constructor
    constructor(
        private applicationroleService: ApplicationRoleService,
        private secureStorageService: SecureStorageService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<ApplicationRoleAddModalComponent>,
        private matSnackBar: MatSnackBar) {
    }


    // Life Cicle
    async ngOnInit(): Promise<void> {
        this.CreateForm();
        await this.GetLocalUser();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            Name: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [
                    Validators.required,
                    Validators.pattern(new RegExp(ExpressionAppVariants.AppNameExpression))
                ]),
            ImageUri: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [Validators.required]),
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id,
                [
                    Validators.required
                ])
        });
    }

    // Form Actions
    async onSubmit(viewModel: AddApplicationRole): Promise<void> {
        let applicationrole = await this.applicationroleService.AddApplicationRole(viewModel);

        if (applicationrole) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
        }

        this.dialogRef.close();
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>(VAULT_USER_KEY);;
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }
}
