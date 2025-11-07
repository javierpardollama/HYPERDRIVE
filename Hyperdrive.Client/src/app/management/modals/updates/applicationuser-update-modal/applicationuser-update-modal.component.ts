import { Component, Inject, OnInit } from '@angular/core';

import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { ViewApplicationUser } from '../../../../../viewmodels/views/viewapplicationuser';

import { UpdateApplicationUser } from '../../../../../viewmodels/updates/updateapplicationuser';

import { ApplicationUserService } from '../../../../../services/applicationuser.service';

import { ApplicationRoleService } from '../../../../../services/applicationrole.service';

import { TextAppVariants } from '../../../../../variants/text.app.variants';

import { TimeAppVariants } from '../../../../../variants/time.app.variants';
import { ViewCatalog } from "../../../../../viewmodels/views/viewcatalog";

@Component({
    selector: 'app-applicationuser-update-modal',
    templateUrl: './applicationuser-update-modal.component.html',
    styleUrls: ['./applicationuser-update-modal.component.scss']
})
export class ApplicationUserUpdateModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public applicationroles: ViewCatalog[] = [];


    // Constructor
    constructor(
        private applicationuserService: ApplicationUserService,
        private applicationroleService: ApplicationRoleService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<ApplicationUserUpdateModalComponent>,
        private matSnackBar: MatSnackBar,
        @Inject(MAT_DIALOG_DATA) public data: ViewApplicationUser) {
    }


    // Life Cicle
    async ngOnInit(): Promise<void> {
        this.CreateForm();
        await this.FindAllApplicationRole();

    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            Id: new FormControl<number>(this.data.Id, [Validators.required]),
            ApplicationRolesId: new FormControl<number[]>(this.data.ApplicationRoles.map(({ Id }) => Id), [Validators.required])
        });
    }

    // Form Actions
    async onSubmit(viewModel: UpdateApplicationUser): Promise<void> {
        let applicationuser = await this.applicationuserService.UpdateApplicationUser(viewModel);

        if (applicationuser) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
        }

        this.dialogRef.close();
    }

    async onDelete(viewModel: UpdateApplicationUser): Promise<void> {
        await this.applicationuserService.RemoveApplicationUserById(viewModel.Id);

        this.matSnackBar.open(
            TextAppVariants.AppOperationSuccessCoreText,
            TextAppVariants.AppOkButtonText,
            { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

        this.dialogRef.close();

    }

    // Get Data from Service
    public async FindAllApplicationRole(): Promise<void> {
        this.applicationroles = await this.applicationroleService.FindAllApplicationRole();
    }
}
