import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { DriveItemService } from "../../../../../services/driveitem.service";
import { FormBuilder, FormControl, FormGroup, Validators } from "@angular/forms";
import { MatSnackBar } from "@angular/material/snack-bar";
import { TextAppVariants } from "../../../../../variants/text.app.variants";
import { ExpressionAppVariants } from "../../../../../variants/expression.app.variants";
import { TimeAppVariants } from "../../../../../variants/time.app.variants";
import { AddDriveItem } from "../../../../../viewmodels/additions/adddriveitem";
import { ViewApplicationUser } from "../../../../../viewmodels/views/viewapplicationuser";
import { ViewDriveItem } from 'src/viewmodels/views/viewdriveitem';

@Component({
    selector: 'app-driveitem-folder-add-modal',
    templateUrl: './driveitem-folder-add-modal.component.html',
    styleUrl: './driveitem-folder-add-modal.component.scss'
})
export class DriveItemFolderAddModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public User?: ViewApplicationUser;

    // Constructor
    constructor(
        private driveItemService: DriveItemService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<DriveItemFolderAddModalComponent>,
        private matSnackBar: MatSnackBar,
        @Inject(MAT_DIALOG_DATA) public data: number | undefined) {
    }


    // Life Cicle
    ngOnInit(): void {
        this.GetLocalUser();
        this.CreateForm();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            ParentId: new FormControl<number | undefined>(this.data,
                []),
            FileName: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [
                    Validators.required,
                    Validators.pattern(new RegExp(ExpressionAppVariants.AppNameExpression))
                ]),
            Folder: new FormControl<boolean>(true,
                [Validators.required]),
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id,
                [
                    Validators.required,
                    Validators.pattern(new RegExp(ExpressionAppVariants.AppNameExpression))
                ]),
        });
    }

    // Form Actions
    async onSubmit(viewModel: AddDriveItem): Promise<void> {
        let archive = await this.driveItemService.AddDriveItem(viewModel);

        if (archive) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
        }

        this.dialogRef.close();
    }

    // Get User from Storage
    public GetLocalUser(): void {
        this.User = JSON.parse(sessionStorage.getItem('User')!);
    }
}
