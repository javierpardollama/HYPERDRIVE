import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { SecurityService } from '../../../../services/security.service';

import { SecurityPasswordChange } from '../../../../viewmodels/security/securitypasswordchange';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from '../../../../variants/text.app.variants';
import { TimeAppVariants } from '../../../../variants/time.app.variants';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CryptoService } from 'src/services/crypto.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-changepassword-modal',
    templateUrl: './changepassword-modal.component.html',
    styleUrls: ['./changepassword-modal.component.scss'],
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
export class ChangePasswordModalComponent implements OnInit {
    // DI
    dialogRef = inject<MatDialogRef<ChangePasswordModalComponent>>(MatDialogRef);
    private securityService = inject(SecurityService);
    private cryptoService = inject(CryptoService);
    private formBuilder = inject(FormBuilder);
    private matSnackBar = inject(MatSnackBar);

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

    // Get User from Storage
    public async GetLocalUser(): Promise<void> {
        this.User = await this.cryptoService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id,
                [Validators.required]),
            CurrentPassword: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [Validators.required]),
            NewPassword: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [Validators.required]),
        });
    }

    // Form Actions
    async onSubmit(viewModel: SecurityPasswordChange): Promise<void> {
        let user = await this.securityService.ChangePassword(viewModel);

        if (user) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

            await this.cryptoService.CreateKey(viewModel.NewPassword);
            await this.cryptoService.StoreObject(VaultKeyAppVariants.VAULT_USER_KEY, user);
        }

        this.dialogRef.close();
    }
}
