import {Component, OnInit} from '@angular/core';

import {Router} from '@angular/router';

import {Location} from '@angular/common';

import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

import {SecurityService} from '../../../services/security.service';

import {SecurityPasswordReset} from '../../../viewmodels/security/securitypasswordreset';

import {TextAppVariants} from '../../../variants/text.app.variants';

import {ExpressionAppVariants} from '../../../variants/expression.app.variants';
import {MatSnackBar} from '@angular/material/snack-bar';
import {TimeAppVariants} from '../../../variants/time.app.variants';

@Component({
    selector: 'app-resetpassword',
    templateUrl: './resetpassword.component.html',
    styleUrls: ['./resetpassword.component.scss']
})
export class ResetPasswordComponent implements OnInit {

    public formGroup!: FormGroup;

    // Constructor
    constructor(
        private router: Router,
        private location: Location,
        private securityService: SecurityService,
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
                [Validators.required,
                    Validators.pattern(ExpressionAppVariants.AppMailExpression)]),
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
                {duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks});

            sessionStorage.setItem('User', JSON.stringify(user));

            await this.router.navigate(['/']);
        }
    }

    onBack(): void {
        this.location.back();
    }
}
