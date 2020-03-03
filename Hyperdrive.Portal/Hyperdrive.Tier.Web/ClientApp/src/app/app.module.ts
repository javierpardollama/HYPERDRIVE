import { BrowserModule } from '@angular/platform-browser';

import { NgModule } from '@angular/core';

import {
  FormsModule,
  ReactiveFormsModule
} from '@angular/forms';

import {
  HttpClientModule,
  HTTP_INTERCEPTORS
} from '@angular/common/http';

import { RouterModule } from '@angular/router';

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

// Guards
import { SignInGuard } from './../guards/signin.guard';

// Interceptors
import { AuthInterceptor } from './../interceptors/auth.interceptor';

// App
import { AppComponent } from './app.component';

import { NavMenuComponent } from './nav-menu/nav-menu.component';

import { HomeComponent } from './home/home.component';

// App-Auth
import {
  JoinInAuthComponent
} from './auth/joinin-auth/joinin-auth.component';

import { SignInAuthComponent } from './auth/signin-auth/signin-auth.component';

// App-Security
import {
  ChangePasswordSecurityComponent
} from './security/changepassword-security/changepassword-security.component';

import {
  ResetPasswordSecurityComponent
} from './security/resetpassword-security/resetpassword-security.component';

import {
  ChangeEmailSecurityComponent
} from './security/changeemail-security/changeemail-security.component';

// App-Grid
import {
  ArchiveGridComponent
} from './management/grids/archive-grid/archive-grid.component';

import {
  ArchiveSharedGridComponent
} from './management/grids/archive-shared-grid/archive-shared-grid.component';

// App-Modal-Adition
import {
  ArchiveAddModalComponent
} from './management/modals/additions/archive-add-modal/archive-add-modal.component';

// App-Modal-Update
import {
  ArchiveUpdateModalComponent
} from './management/modals/updates/archive-update-modal/archive-update-modal.component';

@NgModule({
  declarations: [
    // App
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    // App-Auth
    JoinInAuthComponent,
    SignInAuthComponent,
    // App-Security
    ChangePasswordSecurityComponent,
    ResetPasswordSecurityComponent,
    ChangeEmailSecurityComponent,
    // App-Grid
    ArchiveGridComponent,
    ArchiveSharedGridComponent,
    // App-Modal-Adition
    ArchiveAddModalComponent,
    // App-Modal-Update
    ArchiveUpdateModalComponent],
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
    MatSortModule,
    MatFormFieldModule,
    MatTabsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {
        path: '',
        component: HomeComponent,
        pathMatch: 'full',
        canActivate: [SignInGuard]
      },
      // App-Auth
      {
        path: 'auth/joinin',
        component: JoinInAuthComponent,
        pathMatch: 'full'
      },
      {
        path: 'auth/signin',
        component: SignInAuthComponent,
        pathMatch: 'full'
      },
      // App-Security
      {
        path: 'security/changeemail',
        component: ChangeEmailSecurityComponent,
        pathMatch: 'full',
        canActivate: [SignInGuard]
      },
      {
        path: 'security/changepassword',
        component: ChangePasswordSecurityComponent,
        pathMatch: 'full',
        canActivate: [SignInGuard]
      },
      {
        path: 'security/resetpassword',
        component: ResetPasswordSecurityComponent,
        pathMatch: 'full'
      },
      {
        path: 'management/archives',
        component: ArchiveGridComponent,
        pathMatch: 'full',
        canActivate: [SignInGuard]
      },
      {
        path: 'management/sharedarchives',
        component: ArchiveSharedGridComponent,
        pathMatch: 'full',
        canActivate: [SignInGuard]
      },
    ])
  ],
  entryComponents: [
    ArchiveAddModalComponent,
    ArchiveUpdateModalComponent
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true,
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
