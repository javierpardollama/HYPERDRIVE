import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from "@angular/material/dialog";
import { DriveItemService } from "../../../../../services/driveitem.service";
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatSnackBar, MatSnackBarModule } from "@angular/material/snack-bar";
import { TextAppVariants } from "../../../../../variants/text.app.variants";
import { ExpressionAppVariants } from "../../../../../variants/expression.app.variants";
import { TimeAppVariants } from "../../../../../variants/time.app.variants";
import { AddDriveItem } from "../../../../../viewmodels/additions/adddriveitem";
import { ViewApplicationUser } from "../../../../../viewmodels/views/viewapplicationuser";
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SecureStorageService } from 'src/services/secure.storage.service';

@Component({
    selector: 'app-driveitem-folder-add-modal',
    templateUrl: './driveitem-folder-add-modal.component.html',
    styleUrl: './driveitem-folder-add-modal.component.scss',
    imports: [
        FormsModule,
        MatButtonModule,
        MatDialogModule,
        MatSnackBarModule,
        MatInputModule,
        MatFormFieldModule,
        ReactiveFormsModule
    ]
})
export class DriveItemFolderAddModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public User?: ViewApplicationUser;

    // Constructor
    constructor(
        private driveItemService: DriveItemService,
        private secureStorageService: SecureStorageService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<DriveItemFolderAddModalComponent>,
        private matSnackBar: MatSnackBar,
        @Inject(MAT_DIALOG_DATA) public data: number | undefined) {
    }


    // Life Cicle
    async ngOnInit(): Promise<void> {
        this.CreateForm();
        await this.GetLocalUser();
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
                    Validators.required
                ])
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
    public async GetLocalUser(): Promise<void> {
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>('User');;
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }
}
