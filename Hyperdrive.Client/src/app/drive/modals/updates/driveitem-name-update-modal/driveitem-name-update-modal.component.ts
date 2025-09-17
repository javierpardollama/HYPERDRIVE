import {Component, Inject, OnInit} from '@angular/core';
import {MatButton} from "@angular/material/button";
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {MatOption} from "@angular/material/core";
import {MatSelect} from "@angular/material/select";
import {NgForOf, NgIf, NgOptimizedImage} from "@angular/common";
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {ApplicationRoleService} from "../../../../../services/applicationrole.service";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ViewApplicationRole} from "../../../../../viewmodels/views/viewapplicationrole";
import {ExpressionAppVariants} from "../../../../../variants/expression.app.variants";
import {UpdateApplicationRole} from "../../../../../viewmodels/updates/updateapplicationrole";
import {TextAppVariants} from "../../../../../variants/text.app.variants";
import {TimeAppVariants} from "../../../../../variants/time.app.variants";
import {ViewDriveItem} from "../../../../../viewmodels/views/viewdriveitem";
import {DriveItemService} from "../../../../../services/driveitem.service";
import {UpdateDriveItemName} from "../../../../../viewmodels/updates/updatedriveitemname";

@Component({
  selector: 'app-driveitem-name-update-modal',
  templateUrl: './driveitem-name-update-modal.component.html',
  styleUrl: './driveitem-name-update-modal.component.scss'
})
export class DriveitemNameUpdateModalComponent  implements OnInit {

    public formGroup!: FormGroup;

    // Constructor
    constructor(
        private driveItemService: DriveItemService,
        private formBuilder: FormBuilder,
        public dialogRef: MatDialogRef<DriveitemNameUpdateModalComponent>,
        private matSnackBar: MatSnackBar,
        @Inject(MAT_DIALOG_DATA) public data: ViewDriveItem) { }


    // Life Cicle
    ngOnInit(): void {
        this.CreateForm();
    }

    // Form
    CreateForm(): void {
        this.formGroup = this.formBuilder.group({
            Id: new FormControl<number>(this.data.Id, [Validators.required]),
            Name: new FormControl<string>(this.data.Name, [
                Validators.required,
                Validators.pattern(new RegExp(ExpressionAppVariants.AppNameExpression))
            ]),
            Extension: new FormControl<string>(this.data.Extension, [Validators.required]),
        });
    }

    // Form Actions
    async onSubmit(viewModel: UpdateDriveItemName): Promise<void> {
        let item = await this.driveItemService.UpdateDriveItemName(viewModel);

        if (item) {
            this.matSnackBar.open(
                TextAppVariants.AppOperationSuccessCoreText,
                TextAppVariants.AppOkButtonText,
                { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
        }

        this.dialogRef.close();
    }
}
