import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { ViewApplicationUser } from "../../../../../viewmodels/views/viewapplicationuser";
import { DriveItemService } from "../../../../../services/driveitem.service";
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from "@angular/material/dialog";
import { MatSnackBar, MatSnackBarModule } from "@angular/material/snack-bar";
import { TextAppVariants } from "../../../../../variants/text.app.variants";
import { TimeAppVariants } from "../../../../../variants/time.app.variants";
import { BinaryService } from "../../../../../services/binary.service";
import { BinaryAddDriveItem } from "../../../../../viewmodels/binary/binaryadddriveitem";
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CryptoService } from 'src/services/crypto.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-driveitem-file-add-modal',
    templateUrl: './driveitem-file-add-modal.component.html',
    styleUrl: './driveitem-file-add-modal.component.scss',
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
export class DriveItemFileAddModalComponent implements OnInit {
    // DI
    private driveItemService = inject(DriveItemService);
    private binaryService = inject(BinaryService);
    private cryptoService = inject(CryptoService);
    private formBuilder = inject(FormBuilder);
    dialogRef = inject<MatDialogRef<DriveItemFileAddModalComponent>>(MatDialogRef);
    private matSnackBar = inject(MatSnackBar);
    data = inject(MAT_DIALOG_DATA);

    public formGroup!: FormGroup;

    public User?: ViewApplicationUser;

    public File?: File;

    // Constructor
    constructor() {
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
            File: new FormControl<File | undefined>(undefined,
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
    onSelectFile(file?: File) {
        this.File = file;
    }

    async onSubmit(binary: BinaryAddDriveItem): Promise<void> {

        binary.File = this.File!;

        let viewModel = await this.binaryService.EncodeDriveItem(binary);

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
        this.User = await this.cryptoService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);;
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }
}
