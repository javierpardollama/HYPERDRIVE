import {
  Component,
  OnInit
} from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';

import { SecurityService } from './../../../services/security.service';

import { SecurityEmailChange } from './../../../viewmodels/security/securityemailchange';

import { ViewApplicationUser } from './../../../viewmodels/views/viewapplicationuser';

import { TextAppVariants } from './../../../variants/text.app.variants';

import { ExpressionAppVariants } from './../../../variants/expression.app.variants';

@Component({
  selector: 'app-changeemail-security',
  templateUrl: './changeemail-security.component.html',
  styleUrls: ['./changeemail-security.component.css']
})
export class ChangeEmailSecurityComponent implements OnInit {

  public formGroup: FormGroup;

  public User: ViewApplicationUser;

  // Constructor
  constructor(
    private securityService: SecurityService,
    private formBuilder: FormBuilder) { }

  // Life Cicle
  ngOnInit() {
    this.GetLocalUser();
    this.CreateForm();
  }

  // Form
  CreateForm() {
    this.formGroup = this.formBuilder.group({
      ApplicationUser: [this.User,
      Validators.required],
      NewEmail: [TextAppVariants.AppEmptyCoreText,
      [Validators.required,
      Validators.pattern(ExpressionAppVariants.AppMailExpression)]],
    });
  }

  // Form Actions
  onSubmit(viewModel: SecurityEmailChange) {
    this.securityService.ChangeEmail(viewModel).subscribe(user => { localStorage.setItem('User', JSON.stringify(user)); });
  }

  // Get User from Storage
  public GetLocalUser() {
    this.User = JSON.parse(localStorage.getItem('User'));
  }
}
