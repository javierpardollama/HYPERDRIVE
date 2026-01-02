import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

import { SecurityService } from '../../../../services/security.service';

import { SecurityEmailChange } from '../../../../viewmodels/security/securityemailchange';

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
    selector: 'app-changeemail-modal',
    templateUrl: './changeemail-modal.component.html',
    styleUrls: ['./changeemail-modal.component.scss'],
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
export class ChangeEmailModalComponent implements OnInit {
    // DI
    dialogRef = inject<MatDialogRef<ChangeEmailModalComponent>>(MatDialogRef);
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
        this.User = await this.cryptoService.RetrieveObject<ViewApplicationUser>(VaultKeyAppVariants.VAULT_USER_KEY);;
        this.formGroup.patchValue({ ApplicationUserId: this.User?.Id });
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            ApplicationUserId: new FormControl<number | undefined>(this.User?.Id,
                [Validators.required]),
            NewEmail: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [
                    Validators.required,
                    Validators.email
                ]),
        });
    }

    // Form Actions
    async onSubmit(viewModel: SecurityEmailChange): Promise<void> {
        let user = await this.securityService.ChangeEmail(viewModel);

        if (user) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

            await this.cryptoService.StoreObject(VaultKeyAppVariants.VAULT_USER_KEY, user);
        }

        this.dialogRef.close();
    }
}
