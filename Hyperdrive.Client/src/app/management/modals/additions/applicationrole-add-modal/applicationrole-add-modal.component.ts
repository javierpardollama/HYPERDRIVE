import {Component, OnInit} from '@angular/core';

import {MatDialogRef} from '@angular/material/dialog';
import {MatSnackBar} from '@angular/material/snack-bar';

import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';

import {AddApplicationRole} from '../../../../../viewmodels/additions/addapplicationrole';

import {ApplicationRoleService} from '../../../../../services/applicationrole.service';

import {TextAppVariants} from '../../../../../variants/text.app.variants';

import {TimeAppVariants} from '../../../../../variants/time.app.variants';

import {ExpressionAppVariants} from '../../../../../variants/expression.app.variants';

@Component({
    selector: 'app-applicationrole-add-modal',
    templateUrl: './applicationrole-add-modal.component.html',
    styleUrls: ['./applicationrole-add-modal.component.scss']
})
export class ApplicationRoleAddModalComponent implements OnInit {

    public formGroup!: FormGroup;

    // Constructor
    constructor(
        private applicationroleService: ApplicationRoleService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<ApplicationRoleAddModalComponent>,
        private matSnackBar: MatSnackBar) {
    }


    // Life Cicle
    ngOnInit(): void {
        this.CreateForm();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            Name: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [
                    Validators.required,
                    Validators.pattern(new RegExp(ExpressionAppVariants.AppNameExpression))
                ]),
            ImageUri: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
                [Validators.required]),
        });
    }

    // Form Actions
    async onSubmit(viewModel: AddApplicationRole): Promise<void> {
        let applicationrole = await this.applicationroleService.AddApplicationRole(viewModel);

        if (applicationrole) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                {duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks});
        }

        this.dialogRef.close();
    }
}
