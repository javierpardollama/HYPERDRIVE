import {
  Component,
  OnInit,
  Inject
} from '@angular/core';

import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';

import { ViewApplicationRole } from './../../../../../viewmodels/views/viewapplicationrole';

import { UpdateApplicationRole } from './../../../../../viewmodels/updates/updateapplicationrole';

import { ApplicationRoleService } from './../../../../../services/applicationrole.service';

import { TextAppVariants } from './../../../../../variants/text.app.variants';

import { TimeAppVariants } from './../../../../../variants/time.app.variants';

import { ExpressionAppVariants } from './../../../../../variants/expression.app.variants';

@Component({
  selector: 'app-applicationrole-update-modal',
  templateUrl: './applicationrole-update-modal.component.html',
  styleUrls: ['./applicationrole-update-modal.component.scss']
})
export class ApplicationRoleUpdateModalComponent implements OnInit {

  public formGroup!: FormGroup;

  // Constructor
  constructor(
    private applicationroleService: ApplicationRoleService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<ApplicationRoleUpdateModalComponent>,
    private matSnackBar: MatSnackBar,
    @Inject(MAT_DIALOG_DATA) public data: ViewApplicationRole) { }


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
      ImageUri: new FormControl<string>(this.data.ImageUri, [Validators.required]),
    });
  }

  // Form Actions
  async onSubmit(viewModel: UpdateApplicationRole): Promise<void> {
    let applicationrole = await this.applicationroleService.UpdateApplicationRole(viewModel);

    if (applicationrole) {
      this.matSnackBar.open(
        TextAppVariants.AppOperationSuccessCoreText,
        TextAppVariants.AppOkButtonText,
        { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });
    }

    this.dialogRef.close();

  }

  async onDelete(viewModel: UpdateApplicationRole): Promise<void> {
    await this.applicationroleService.RemoveApplicationRoleById(viewModel.Id);

    this.matSnackBar.open(
      TextAppVariants.AppOperationSuccessCoreText,
      TextAppVariants.AppOkButtonText,
      { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

    this.dialogRef.close();
  }
}
