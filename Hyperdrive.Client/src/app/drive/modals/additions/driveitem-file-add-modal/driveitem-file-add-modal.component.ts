import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from "@angular/forms";
import {ViewApplicationUser} from "../../../../../viewmodels/views/viewapplicationuser";
import {DriveItemService} from "../../../../../services/driveitem.service";
import {MatDialogRef} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {TextAppVariants} from "../../../../../variants/text.app.variants";
import {TimeAppVariants} from "../../../../../variants/time.app.variants";
import {BinaryService} from "../../../../../services/binary.service";
import {BinaryAddDriveItem} from "../../../../../viewmodels/binary/binaryadddriveitem";

@Component({
  selector: 'app-driveitem-file-add-modal',
  templateUrl: './driveitem-file-add-modal.component.html',
  styleUrl: './driveitem-file-add-modal.component.scss'
})
export class DriveItemFileAddModalComponent implements OnInit {

    public formGroup!: FormGroup;

    public User?: ViewApplicationUser;

    // Constructor
    constructor(
        private driveItemService: DriveItemService,
        private binaryService: BinaryService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<DriveItemFileAddModalComponent>,
        private matSnackBar: MatSnackBar) {
    }


    // Life Cicle
    ngOnInit(): void {
        this.GetLocalUser();
        this.CreateForm();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            ParentId: new FormControl<number | undefined>(undefined,
                []),
            Data: new FormControl<File | undefined>(undefined,
                [
                    Validators.required
                ]),
            Folder: new FormControl<boolean>(false,
                [Validators.required]),
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id,
                [
                    Validators.required
                ])
        });
    }

    // Form Actions
    async onSubmit(binary: BinaryAddDriveItem): Promise<void> {
        let viewModel = await this.binaryService.EncodeAddDriveItem(binary);

        let archive = await this.driveItemService.AddDriveItem(viewModel);

        if (archive) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                {duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks});
        }

        this.dialogRef.close();
    }

    // Get User from Storage
    public GetLocalUser() {
        this.User = JSON.parse(sessionStorage.getItem('User')!);
    }
}
