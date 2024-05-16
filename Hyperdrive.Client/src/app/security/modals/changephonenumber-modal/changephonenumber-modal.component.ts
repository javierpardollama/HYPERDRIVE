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

import { SecurityService } from '../../../../services/security.service';

import { SecurityPhoneNumberChange } from '../../../../viewmodels/security/securityphonenumberchange';

import { ViewApplicationUser } from '../../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from '../../../../variants/text.app.variants';

import { TimeAppVariants } from '../../../../variants/time.app.variants';

@Component({
  selector: 'app-changephonenumber-modal',
  templateUrl: './changephonenumber-modal.component.html',
  styleUrls: ['./changephonenumber-modal.component.scss']
})
export class ChangePhoneNumberModalComponent implements OnInit {

  public formGroup!: FormGroup;

  public User!: ViewApplicationUser;

  // Constructor
  constructor(
    public dialogRef: MatDialogRef<ChangePhoneNumberModalComponent>,
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
      ApplicationUserId: [this.User.Id,
      Validators.required],
      NewPhoneNumber: [TextAppVariants.AppEmptyCoreText,
      [
        Validators.required,
      ]],
    });
  }

  // Form Actions
  async onSubmit(viewModel: SecurityPhoneNumberChange) {
    let user = await this.securityService.ChangePhoneNumber(viewModel);

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
