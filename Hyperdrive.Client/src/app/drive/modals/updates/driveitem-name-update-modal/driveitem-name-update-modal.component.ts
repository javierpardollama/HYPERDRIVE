import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ExpressionAppVariants } from "../../../../../variants/expression.app.variants";
import { TextAppVariants } from "../../../../../variants/text.app.variants";
import { TimeAppVariants } from "../../../../../variants/time.app.variants";
import { ViewDriveItem } from "../../../../../viewmodels/views/viewdriveitem";
import { DriveItemService } from "../../../../../services/driveitem.service";
import { UpdateDriveItemName } from "../../../../../viewmodels/updates/updatedriveitemname";
import { ViewApplicationUser } from 'src/viewmodels/views/viewapplicationuser';
import { DecryptObject } from 'src/utils/crypto.utils';

@Component({
    selector: 'app-driveitem-name-update-modal',
    templateUrl: './driveitem-name-update-modal.component.html',
    styleUrl: './driveitem-name-update-modal.component.scss'
})
export class DriveitemNameUpdateModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public User?: ViewApplicationUser;

    // Constructor
    constructor(
        private driveItemService: DriveItemService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<DriveitemNameUpdateModalComponent>,
        private matSnackBar: MatSnackBar,
        @Inject(MAT_DIALOG_DATA) public data: ViewDriveItem) {
    }


    // Life Cicle
    async ngOnInit(): Promise<void> {
        await this.GetLocalUser();
        this.CreateForm();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            Id: new FormControl<number>(this.data.Id, [Validators.required]),
            Name: new FormControl<string>(this.data.Name, [
                Validators.required,
                Validators.pattern(new RegExp(ExpressionAppVariants.AppNameExpression))
            ]),
            Extension: new FormControl<string>(this.data.Extension, [Validators.required]),
            ParentId: new FormControl<number | undefined>(this.data?.Parent?.Id,
                []),
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id,
                [
                    Validators.required
                ])
        });
    }

    // Form Actions
    async onSubmit(viewModel: UpdateDriveItemName): Promise<void> {
        let item = await this.driveItemService.UpdateDriveItemName(viewModel);

        if (item) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
        }

        this.dialogRef.close();
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await DecryptObject(sessionStorage.getItem('User')!) as ViewApplicationUser;
    }
}
