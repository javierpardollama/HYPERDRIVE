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

import { SecurityPasswordChange } from './../../../viewmodels/security/securitypasswordchange';

import { ViewApplicationUser } from './../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from './../../../variants/text.app.variants';

import { TimeAppVariants } from 'src/variants/time.app.variants';

@Component({
  selector: 'app-changepassword-security',
  templateUrl: './changepassword-security.component.html',
  styleUrls: ['./changepassword-security.component.scss']
})
export class ChangePasswordSecurityComponent implements OnInit {

  public formGroup: FormGroup;

  public User: ViewApplicationUser;

  // Constructor
  constructor(
    public dialogRef: MatDialogRef<ChangePasswordSecurityComponent>,
    private securityService: SecurityService,
    private formBuilder: FormBuilder,
    private matSnackBar: MatSnackBar) { }

  // Life Cicle
  ngOnInit() {
    this.User = JSON.parse(localStorage.getItem('User'));

    this.CreateForm();
  }

  // Form
  CreateForm() {
    this.formGroup = this.formBuilder.group({
      ApplicationUser: [this.User,
      Validators.required],
      CurrentPassword: [TextAppVariants.AppEmptyCoreText,
      [Validators.required]],
      NewPassword: [TextAppVariants.AppEmptyCoreText,
      [Validators.required]]
    });
  }

  // Form Actions
  async onSubmit(viewModel: SecurityPasswordChange) {
    let user = await this.securityService.ChangePassword(viewModel);

    if (user !== undefined) {
      this.matSnackBar.open(
        TextAppVariants.AppOperationSuccessCoreText,
        TextAppVariants.AppOkButtonText,
        { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

        localStorage.setItem('User', JSON.stringify(user));
    }
    
    this.dialogRef.close();
  }
}
