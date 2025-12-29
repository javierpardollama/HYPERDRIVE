import { ChangeDetectionStrategy, Component, OnInit, inject } from '@angular/core';

import { Router, RouterModule } from '@angular/router';

import { FormBuilder, FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { AuthService } from '../../../services/auth.service';

import { AuthSignIn } from '../../../viewmodels/auth/authsignin';

import { TextAppVariants } from '../../../variants/text.app.variants';
import { Location } from "@angular/common";
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SecureStorageService } from 'src/services/secure.storage.service';
import { VaultKeyAppVariants } from 'src/variants/vault.keys.variants';

@Component({
    changeDetection: ChangeDetectionStrategy.OnPush,
    selector: 'app-joinin-auth',
    templateUrl: './joinin.component.html',
    styleUrls: ['./joinin.component.scss'],
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
export class JoinInComponent implements OnInit {
    // DI
    private location = inject(Location);
    private router = inject(Router);
    private authService = inject(AuthService);
    private secureStorageService = inject(SecureStorageService);
    private formBuilder = inject(FormBuilder);

    public formGroup!: FormGroup;

    // Constructor
    constructor() {
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
            Password: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [Validators.required])
        });
    }

    // Form Actions
    async onSubmit(viewModel: AuthSignIn): Promise<void> {
        let user = await this.authService.JoinIn(viewModel);

        if (user) {
            await this.secureStorageService.CreateKey(viewModel.Password);
            await this.secureStorageService.StoreObject(VaultKeyAppVariants.VAULT_USER_KEY, user);

            await this.router.navigate(['/']);
        }
    }

    onBack(): void {
        this.location.back();
    }
}
