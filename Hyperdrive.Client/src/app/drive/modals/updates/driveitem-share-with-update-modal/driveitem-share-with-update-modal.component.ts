import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { ApplicationUserService } from "../../../../../services/applicationuser.service";
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar, MatSnackBarModule } from "@angular/material/snack-bar";
import { UpdateApplicationUser } from "../../../../../viewmodels/updates/updateapplicationuser";
import { TextAppVariants } from "../../../../../variants/text.app.variants";
import { TimeAppVariants } from "../../../../../variants/time.app.variants";
import { ViewDriveItem } from "../../../../../viewmodels/views/viewdriveitem";
import { ViewCatalog } from "../../../../../viewmodels/views/viewcatalog";
import { DriveItemService } from "../../../../../services/driveitem.service";
import { UpdateDriveItemSharedWith } from "../../../../../viewmodels/updates/updatedriveitemsharedwith";
import { ViewApplicationUser } from 'src/viewmodels/views/viewapplicationuser';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';

@Component({
    selector: 'app-driveitem-share-with-update-modal',
    templateUrl: './driveitem-share-with-update-modal.component.html',
    styleUrl: './driveitem-share-with-update-modal.component.scss',
    imports: [
        FormsModule,
        MatButtonModule,
        MatDialogModule,
        MatSnackBarModule,
        MatInputModule,
        MatSelectModule,
        MatFormFieldModule,
        ReactiveFormsModule
    ]
})
export class DriveitemShareWithUpdateModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public User?: ViewApplicationUser;

    public applicationusers: ViewCatalog[] = [];


    // Constructor
    constructor(
        private applicationuserService: ApplicationUserService,
        private driveItemService: DriveItemService,
        private secureStorageService: SecureStorageService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<DriveitemShareWithUpdateModalComponent>,
        private matSnackBar: MatSnackBar,
        @Inject(MAT_DIALOG_DATA) public data: ViewDriveItem) {
    }


    // Life Cicle
    async ngOnInit(): Promise<void> {
        this.CreateForm();
        await this.GetLocalUser()
        await this.FindAllApplicationUser();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            Id: new FormControl<number>(this.data.Id, [Validators.required]),
            ApplicationUserIds: new FormControl<number[]>(this.data.SharedWith.map(({ Id }) => Id), [Validators.required]),
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id,
                [
                    Validators.required
                ])
        });
    }

    // Form Actions
    async onSubmit(viewModel: UpdateDriveItemSharedWith): Promise<void> {
        let item = await this.driveItemService.UpdateDriveItemSharedWith(viewModel);

        if (item) {
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
    public async FindAllApplicationUser(): Promise<void> {
        this.applicationusers = await this.applicationuserService.FindAllApplicationUser();
    }

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.secureStorageService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);;
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }
}
