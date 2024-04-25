import {
  Component,
  OnInit
} from '@angular/core';

import { Location } from '@angular/common';

import {
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';

import { SecurityService } from '../../../services/security.service';

import { SecurityPasswordReset } from '../../../viewmodels/security/securitypasswordreset';

import { TextAppVariants } from '../../../variants/text.app.variants';

import { ExpressionAppVariants } from '../../../variants/expression.app.variants';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TimeAppVariants } from '../../../variants/time.app.variants';

@Component({
  selector: 'app-resetpassword-auth',
  templateUrl: './resetpassword-auth.component.html',
  styleUrls: ['./resetpassword-auth.component.scss']
})
export class ResetPasswordAuthComponent implements OnInit {

  public formGroup!: FormGroup;

  // Constructor
  constructor(
    private location: Location,   
    private securityService: SecurityService,
    private formBuilder: FormBuilder,
    private matSnackBar: MatSnackBar) { }

  // Life Cicle
  ngOnInit() {
    this.CreateForm();
  }

  // Form
  CreateForm() {
    this.formGroup = this.formBuilder.group({
      Email: [TextAppVariants.AppEmptyCoreText,
      [Validators.required,
      Validators.pattern(ExpressionAppVariants.AppMailExpression)]],
      NewPassword: [TextAppVariants.AppEmptyCoreText,
      [Validators.required]]
    });
  }

  // Form Actions
  async onSubmit(viewModel: SecurityPasswordReset) {
    let user = await this.securityService.ResetPassword(viewModel);

    if (user) {
      this.matSnackBar.open(
        TextAppVariants.AppOperationSuccessCoreText,
        TextAppVariants.AppOkButtonText,
        { duration: TimeAppVariants.AppToastSecondTicks * TimeAppVariants.AppTimeSecondTicks });

      sessionStorage.setItem('User', JSON.stringify(user));
    }
  }
 
  onBack() {
    this.location.back();;
  }
}
