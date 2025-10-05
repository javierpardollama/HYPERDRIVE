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
import {Location} from "@angular/common";

@Component({
  selector: 'app-joinin-auth',
  templateUrl: './joinin.component.html',
  styleUrls: ['./joinin.component.scss']
})
export class JoinInComponent implements OnInit {

  public formGroup!: FormGroup;

  // Constructor
  constructor(
    private location: Location,
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
      Email: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
        [
          Validators.required,
        ]),
      Password: new FormControl<string>(TextAppVariants.AppEmptyCoreText,
        [Validators.required])
    });
  }

  // Form Actions
  async onSubmit(viewModel: AuthSignIn) {
    let user = await this.authService.JoinIn(viewModel);

    if (user) {
      sessionStorage.setItem('User', JSON.stringify(user));

      await this.router.navigate(['/']);
    }
  }

    onBack() {
        this.location.back();
    }
}
