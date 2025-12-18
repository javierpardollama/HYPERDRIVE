import { Component, OnInit } from '@angular/core';

import { Router, RouterModule } from '@angular/router';

import { Location } from '@angular/common';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { SecurityService } from '../../../services/security.service';

import { SecurityPasswordReset } from '../../../viewmodels/security/securitypasswordreset';

import { TextAppVariants } from '../../../variants/text.app.variants';

import { MatSnackBar } from '@angular/material/snack-bar';
import { TimeAppVariants } from '../../../variants/time.app.variants';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';

@Component({
    selector: 'app-resetpassword',
    templateUrl: './resetpassword.component.html',
    styleUrls: ['./resetpassword.component.scss'],
    imports: [
        FormsModule,
        MatButtonModule,
        MatInputModule,
        MatFormFieldModule,
        ReactiveFormsModule,
        RouterModule,
        MatTooltipModule
    ]
})
export class ResetPasswordComponent implements OnInit {

    public formGroup!: FormGroup;

    // Constructor
    constructor(
        private router: Router,
        private location: Location,
        private securityService: SecurityService,
        private secureStorageService: SecureStorageService,
        private formBuilder: FormBuilder,
        private matSnackBar: MatSnackBar) {
    }

    // Life Cicle
    ngOnInit(): void {
        this.CreateForm();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            Email: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [
                    Validators.required,
                    Validators.email
                ]),
            NewPassword: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [Validators.required])
        });
    }

    // Form Actions
    async onSubmit(viewModel: SecurityPasswordReset): Promise<void> {
        let user = await this.securityService.ResetPassword(viewModel);

        if (user) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

            await this.secureStorageService.CreateKey(viewModel.NewPassword);
            await this.secureStorageService.StoreObject(VaultKeyAppVariants.VAULT_USER_KEY, user);

            await this.router.navigate(['/']);
        }
    }

    onBack(): void {
        this.location.back();
    }
}
