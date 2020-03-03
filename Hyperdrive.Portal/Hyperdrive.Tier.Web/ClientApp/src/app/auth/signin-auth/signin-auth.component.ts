import {
  Component,
  OnInit
} from '@angular/core';

import { Router } from '@angular/router';

import {
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';

import { AuthService } from './../../../services/auth.service';

import { AuthSignIn } from './../../../viewmodels/auth/authsignin';

import { TextAppVariants } from './../../../variants/text.app.variants';

import { ExpressionAppVariants } from './../../../variants/expression.app.variants';

@Component({
  selector: 'app-signin-auth',
  templateUrl: './signin-auth.component.html',
  styleUrls: ['./signin-auth.component.css']
})
export class SignInAuthComponent implements OnInit {

  public formGroup: FormGroup;

  // Constructor
  constructor(
    private router: Router,
    private authService: AuthService,
    private formBuilder: FormBuilder) { }

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
      Password: [TextAppVariants.AppEmptyCoreText,
      [Validators.required]]
    });
  }

  // Form Actions
  onSubmit(viewModel: AuthSignIn) {
    this.authService.SignIn(viewModel).subscribe(user => { localStorage.setItem('User', JSON.stringify(user)); });
  }

  onNavigate() {
    this.router.navigate(['/auth/joinin']);
  }
}
