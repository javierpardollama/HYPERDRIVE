import {
  Component,
  OnInit
} from '@angular/core';

import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';

import { MatDialogRef } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

import { SecurityService } from '../../../../services/security.service';

import { SecurityPasswordChange } from '../../../../viewmodels/security/securitypasswordchange';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from '../../../../variants/text.app.variants';
import { TimeAppVariants } from '../../../../variants/time.app.variants';

@Component({
  selector: 'app-changepassword-modal',
  templateUrl: './changepassword-modal.component.html',
  styleUrls: ['./changepassword-modal.component.scss']
})
export class ChangePasswordModalComponent implements OnInit {

  public formGroup!: FormGroup;

  public User!: ViewApplicationUser;

  // Constructor
  constructor(
    public dialogRef: MatDialogRef<ChangePasswordModalComponent>,
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
    this.User = JSON.parse(sessionStorage.getItem('User')!);
  }

  // Form
  CreateForm() {
    this.formGroup = this.formBuilder.group({
      ApplicationUserId: new FormControl<number>(this.User.Id,
        [Validators.required]),
      CurrentPassword: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
        [Validators.required]),
      NewPassword: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
        [Validators.required])
    });
  }

  // Form Actions
  async onSubmit(viewModel: SecurityPasswordChange) {
    let user = await this.securityService.ChangePassword(viewModel);

    if (user) {
      this.matSnackBar.open(
        TextAppVariants.AppOperationSuccessCoreText,
        TextAppVariants.AppOkButtonText,
        { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

      sessionStorage.setItem('User', JSON.stringify(user));
    }

    this.dialogRef.close();
  }
}
