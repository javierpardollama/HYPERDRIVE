import {
  Component,
  OnInit
} from '@angular/core';

import { Router } from '@angular/router';

import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from '@angular/forms';

import { AuthService } from '../../../services/auth.service';

import { AuthSignIn } from '../../../viewmodels/auth/authsignin';

import { TextAppVariants } from '../../../variants/text.app.variants';
import { EncryptObject } from 'src/utils/crypto.utils';


@Component({
    selector: 'app-signin',
    templateUrl: './signin.component.html',
    styleUrls: ['./signin.component.scss'],
    standalone: false
})
export class SignInComponent implements OnInit {

  public formGroup!: FormGroup;

  // Constructor
  constructor(
    private router: Router,
    private authService: AuthService,
    private formBuilder: FormBuilder) { }

  // Life Cicle
  ngOnInit(): void {
    this.CreateForm();
  }

  // Form
  CreateForm(): void {
    this.formGroup = this.formBuilder.group({
      Email: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
        [
          Validators.required,
          Validators.email
        ]),
      Password: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
        [Validators.required])
    });
  }

  // Form Actions
  async onSubmit(viewModel: AuthSignIn): Promise<void> {
    let user = await this.authService.SignIn(viewModel);

    if (user) {
      sessionStorage.setItem('User', await EncryptObject(user));

      await this.router.navigate(['/']);
    }
  }
}
