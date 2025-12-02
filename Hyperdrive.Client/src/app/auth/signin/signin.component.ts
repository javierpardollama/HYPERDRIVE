import {
  Component,
  OnInit
} from '@angular/core';

import { Router, RouterModule } from '@angular/router';

import {
  FormBuilder,
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { AuthService } from '../../../services/auth.service';

import { AuthSignIn } from '../../../viewmodels/auth/authsignin';

import { TextAppVariants } from '../../../variants/text.app.variants';
import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SecureStorageService } from 'src/services/secure.storage.service';


@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.scss'],
  imports: [
    FormsModule,
    MatButtonModule,
    MatInputModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    RouterModule,
    MatTooltipModule
  ]
})
export class SignInComponent implements OnInit {

  public formGroup!: FormGroup;

  // Constructor
  constructor(
    private router: Router,
    private authService: AuthService,
    private secureStorageService: SecureStorageService,
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
      await this.secureStorageService.CreateKey(viewModel.Password);
      await this.secureStorageService.StoreObject('User', user);

      await this.router.navigate(['/']);
    }
  }
}
