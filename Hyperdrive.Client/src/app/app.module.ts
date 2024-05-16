import { BrowserModule } from '@angular/platform-browser';

import { NgOptimizedImage } from '@angular/common';

import { NgModule } from '@angular/core';

import {
  FormsModule,
  ReactiveFormsModule
} from '@angular/forms';

import {
  HttpClientModule,
  HTTP_INTERCEPTORS
} from '@angular/common/http';


// Angular Material
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSelectModule } from '@angular/material/select';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatExpansionModule } from '@angular/material/expansion'

// Interceptors
import { AuthInterceptor } from '../interceptors/auth.interceptor';

// App
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { UnknownComponent } from './unknown/unknown.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';

// App-Nav
import { NavMenuComponent }
  from './nav-menu/nav-menu.component';
import { ProfileModalComponent }
  from './nav-menu/modals/profile-modal/profile-modal.component';
import { ToolboxModalComponent }
  from './nav-menu/modals/toolbox-modal/toolbox-modal.component';

// App-Auth
import {
  JoinInAuthComponent
} from './auth/joinin-auth/joinin-auth.component';
import { SignInAuthComponent }
  from './auth/signin-auth/signin-auth.component';

// App-Security
import { SecurityComponent }
  from './security/security.component'
import {
  ChangePasswordModalComponent
} from './security/modals/changepassword-modal/changepassword-modal.component';
import {
  ResetPasswordAuthComponent
} from './auth/resetpassword-auth/resetpassword-auth.component';
import { ChangePhoneNumberModalComponent }
  from './security/modals/changephonenumber-modal/changephonenumber-modal.component';
import {
  ChangeEmailModalComponent
} from './security/modals/changeemail-modal/changeemail-modal.component';
import {
  ChangeNameModalComponent
} from './security/modals/changename-modal/changename-modal.component';

// App-Grid
import { ApplicationRoleGridComponent }
  from './management/grids/applicationrole-grid/applicationrole-grid.component';
import { ApplicationUserGridComponent }
  from './management/grids/applicationuser-grid/applicationuser-grid.component';

// App-Modal-Adition
import { ApplicationRoleAddModalComponent }
  from './management/modals/additions/applicationrole-add-modal/applicationrole-add-modal.component';

// App-Modal-Update
import { ApplicationRoleUpdateModalComponent }
  from './management/modals/updates/applicationrole-update-modal/applicationrole-update-modal.component';
import { ApplicationUserUpdateModalComponent }
  from './management/modals/updates/applicationuser-update-modal/applicationuser-update-modal.component';


@NgModule({
  declarations: [
    // App
    AppComponent,
    HomeComponent,
    UnknownComponent,
    UnauthorizedComponent,
    //Nav
    NavMenuComponent,
    ProfileModalComponent,
    ToolboxModalComponent,
    // App-Auth
    JoinInAuthComponent,
    SignInAuthComponent,
    ResetPasswordAuthComponent,
    // App-Security
    SecurityComponent,
    ChangePasswordModalComponent,
    ChangeEmailModalComponent,
    ChangePhoneNumberModalComponent,
    ChangeNameModalComponent,
    // App-Grid
    ApplicationRoleGridComponent,
    ApplicationUserGridComponent,
    // App-Modal-Adition
    ApplicationRoleAddModalComponent,
    // App-Modal-Update
    ApplicationRoleUpdateModalComponent,
    ApplicationUserUpdateModalComponent
  ],
  imports: [
    // Angular Material
    BrowserAnimationsModule,
    MatDividerModule,
    MatSelectModule,
    MatInputModule,
    MatDialogModule,
    MatPaginatorModule,
    MatButtonModule,
    MatSnackBarModule,
    MatChipsModule,
    MatAutocompleteModule,
    MatCardModule,
    MatTableModule,
    MatTabsModule,
    MatSortModule,
    MatFormFieldModule,
    MatExpansionModule,
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgOptimizedImage
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
