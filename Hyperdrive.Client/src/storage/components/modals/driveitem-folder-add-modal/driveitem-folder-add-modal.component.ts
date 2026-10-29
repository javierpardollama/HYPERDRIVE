import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from "@angular/material/dialog";
import { DriveItemService } from "../../../services/driveitem.service";
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from "@angular/forms";
import { MatSnackBar, MatSnackBarModule } from "@angular/material/snack-bar";
import { TextAppVariants } from "../../../../shared/variants/text.app.variants";
import { ExpressionAppVariants } from "../../../../shared/variants/expression.app.variants";
import { TimeAppVariants } from "../../../../shared/variants/time.app.variants";
import { AddDriveItem } from "../../../viewmodels/additions/adddriveitem";
import { ViewApplicationUser } from "../../../../identity/viewmodels/views/viewapplicationuser";
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CryptoService } from '../../../../identity/services/crypto.service';
import { VaultKeyAppVariants } from '../../../../shared/variants/vault.keys.variants';

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
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
    // DI
    private driveItemService = inject(DriveItemService);
    private cryptoService = inject(CryptoService);
    private formBuilder = inject(FormBuilder);
    dialogRef = inject<MatDialogRef<DriveItemFolderAddModalComponent>>(MatDialogRef);
    private matSnackBar = inject(MatSnackBar);
    data = inject(MAT_DIALOG_DATA);

    public formGroup!: FormGroup;

    public User?: ViewApplicationUser;

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
            ParentId:[this.data,
                []],
            FileName: [TextAppVariants.AppEmptyCoreText,
                [
                    Validators.required,
                    Validators.pattern(new RegExp(ExpressionAppVariants.AppNameExpression))
                ]],
            Folder: [true,
                [Validators.required]],
            ApplicationUserId: [this.User?.Id,
                [
                    Validators.required
                ]]
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
        this.User = await this.cryptoService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }
}
