import {
  Component,
  OnInit
} from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';

import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { SecurityService } from './../../../services/security.service';

import { SecurityEmailChange } from './../../../viewmodels/security/securityemailchange';

import { ViewApplicationUser } from './../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from './../../../variants/text.app.variants';

import { ExpressionAppVariants } from './../../../variants/expression.app.variants';
import { TimeAppVariants } from '../../../variants/time.app.variants';

@Component({
  selector: 'app-changeemail-security',
  templateUrl: './changeemail-security.component.html',
  styleUrls: ['./changeemail-security.component.scss']
})
export class ChangeEmailSecurityComponent implements OnInit {

  public formGroup!: FormGroup;

  public User!: ViewApplicationUser;

  // Constructor
  constructor(
    public dialogRef: MatDialogRef<ChangeEmailSecurityComponent>,
    private securityService: SecurityService,
    private formBuilder: FormBuilder,
    private matSnackBar: MatSnackBar) { }

  // Life Cicle
  ngOnInit() {
    this.GetLocalUser();
    this.CreateForm();
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(localStorage.getItem('User') || TextAppVariants.AppEmptyCoreObject);
  }

  // Form
  CreateForm() {
    this.formGroup = this.formBuilder.group({
      ApplicationUser: [this.User,
      Validators.required],
      NewEmail: [TextAppVariants.AppEmptyCoreText,
      [
        Validators.required,
      ]],
    });
  }

  // Form Actions
  async onSubmit(viewModel: SecurityEmailChange) {
    let user = await this.securityService.ChangeEmail(viewModel);

    if (user) {
      this.matSnackBar.open(
        TextAppVariants.AppOperationSuccessCoreText,
        TextAppVariants.AppOkButtonText,
        { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

      localStorage.setItem('User', JSON.stringify(user));
    }

    this.dialogRef.close();
  }
}
