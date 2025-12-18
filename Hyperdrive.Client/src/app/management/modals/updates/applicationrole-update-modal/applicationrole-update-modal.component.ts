import { Component, Inject, OnInit } from '@angular/core';

import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { ViewApplicationRole } from './../../../../../viewmodels/views/viewapplicationrole';

import { UpdateApplicationRole } from './../../../../../viewmodels/updates/updateapplicationrole';

import { ApplicationRoleService } from './../../../../../services/applicationrole.service';

import { TextAppVariants } from './../../../../../variants/text.app.variants';

import { TimeAppVariants } from './../../../../../variants/time.app.variants';

import { ExpressionAppVariants } from './../../../../../variants/expression.app.variants';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { ViewApplicationUser } from 'src/viewmodels/views/viewapplicationuser';
import { VAULT_USER_KEY } from 'src/variants/vault.keys.variants';

@Component({
    selector: 'app-applicationrole-update-modal',
    templateUrl: './applicationrole-update-modal.component.html',
    styleUrls: ['./applicationrole-update-modal.component.scss'],
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
export class ApplicationRoleUpdateModalComponent implements OnInit {

    public User?: ViewApplicationUser;

    public formGroup!: FormGroup;

    // Constructor
    constructor(
        private applicationroleService: ApplicationRoleService,
        private secureStorageService: SecureStorageService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<ApplicationRoleUpdateModalComponent>,
        private matSnackBar: MatSnackBar,
        @Inject(MAT_DIALOG_DATA) public data: ViewApplicationRole) {
    }


    // Life Cicle
    async ngOnInit(): Promise<void> {
        this.CreateForm();
        await this.GetLocalUser();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            Id: new FormControl<number>(this.data.Id, [Validators.required]),
            Name: new FormControl<string>(this.data.Name, [
                Validators.required,
                Validators.pattern(new RegExp(ExpressionAppVariants.AppNameExpression))
            ]),
            ImageUri: new FormControl<string>(this.data.ImageUri, [Validators.required]),
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id, [
                Validators.required
            ])
        });
    }

    // Form Actions
    async onSubmit(viewModel: UpdateApplicationRole): Promise<void> {
        let applicationrole = await this.applicationroleService.UpdateApplicationRole(viewModel);

        if (applicationrole) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
        }

        this.dialogRef.close();

    }

    async onDelete(viewModel: UpdateApplicationRole): Promise<void> {
        await this.applicationroleService.RemoveApplicationRoleById(viewModel.Id);

        this.matSnackBar.open(
            TextAppVariants.AppOperationSuccessCoreText,
            TextAppVariants.AppOkButtonText,
            { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

        this.dialogRef.close();
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>(VAULT_USER_KEY);;
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }
}
