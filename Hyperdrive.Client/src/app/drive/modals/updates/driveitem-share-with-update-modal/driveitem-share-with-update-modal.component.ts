import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ApplicationUserService} from "../../../../../services/applicationuser.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {UpdateApplicationUser} from "../../../../../viewmodels/updates/updateapplicationuser";
import {TextAppVariants} from "../../../../../variants/text.app.variants";
import {TimeAppVariants} from "../../../../../variants/time.app.variants";
import {ViewDriveItem} from "../../../../../viewmodels/views/viewdriveitem";
import {ViewCatalog} from "../../../../../viewmodels/views/viewcatalog";
import {DriveItemService} from "../../../../../services/driveitem.service";
import {UpdateDriveItemSharedWith} from "../../../../../viewmodels/updates/updatedriveitemsharedwith";

@Component({
    selector: 'app-driveitem-share-with-update-modal',
    templateUrl: './driveitem-share-with-update-modal.component.html',
    styleUrl: './driveitem-share-with-update-modal.component.scss'
})
export class DriveitemShareWithUpdateModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public applicationusers: ViewCatalog[] = [];


    // Constructor
    constructor(
        private applicationuserService: ApplicationUserService,
        private driveItemService: DriveItemService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<DriveitemShareWithUpdateModalComponent>,
        private matSnackBar: MatSnackBar,
        @Inject(MAT_DIALOG_DATA) public data: ViewDriveItem) {
    }


    // Life Cicle
    async ngOnInit(): Promise<void> {
        this.CreateForm();
        await this.FindAllApplicationUser();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            Id: new FormControl<number>(this.data.Id, [Validators.required]),
            ApplicationUserIds: new FormControl<number[]>(this.data.SharedWith.map(({Id}) => Id), [Validators.required])
        });
    }

    // Form Actions
    async onSubmit(viewModel: UpdateDriveItemSharedWith): Promise<void> {
        let item = await this.driveItemService.UpdateDriveItemSharedWith(viewModel);

        if (item) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                {duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks});
        }

        this.dialogRef.close();
    }

    async onDelete(viewModel: UpdateApplicationUser): Promise<void> {
        await this.applicationuserService.RemoveApplicationUserById(viewModel.Id);

        this.matSnackBar.open(
            TextAppVariants.AppOperationSuccessCoreText,
            TextAppVariants.AppOkButtonText,
            {duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks});

        this.dialogRef.close();

    }

    // Get Data from Service
    public async FindAllApplicationUser(): Promise<void> {
        this.applicationusers = await this.applicationuserService.FindAllApplicationUser();
    }
}
