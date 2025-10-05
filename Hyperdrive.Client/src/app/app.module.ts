import { BrowserModule } from '@angular/platform-browser';

import { NgOptimizedImage } from '@angular/common';

import { NgModule } from '@angular/core';

import {
  FormsModule,
  ReactiveFormsModule
} from '@angular/forms';

import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

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
import { MatExpansionModule } from '@angular/material/expansion';
import { MatBottomSheetModule } from "@angular/material/bottom-sheet";
import { MatListModule } from "@angular/material/list";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatMenuModule } from "@angular/material/menu";
import { MatTooltipModule } from "@angular/material/tooltip";

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
  from './shell/menus/nav-menu/nav-menu.component';
import { ProfileContextMenuComponent }
  from './shell/menus/profile-context-menu/profile-context-menu.component';
import { ToolboxContextMenuComponent }
  from './shell/menus/toolbox-context-menu/toolbox-context-menu.component';

// App-Auth
import {
  JoinInComponent
} from './auth/joinin/joinin.component';
import { SignInComponent }
  from './auth/signin/signin.component';

// App-Security
import { SecurityComponent }
  from './security/security.component'
import {
  ChangePasswordModalComponent
} from './security/modals/changepassword-modal/changepassword-modal.component';
import {
  ResetPasswordComponent
} from './auth/resetpassword/resetpassword.component';
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
import { DriveitemGridComponent } from './drive/grids/driveitem-grid/driveitem-grid.component'

// App-Modal-Adition
import { ApplicationRoleAddModalComponent }
  from './management/modals/additions/applicationrole-add-modal/applicationrole-add-modal.component';
import { DriveItemFolderAddModalComponent } from "./drive/modals/additions/driveitem-folder-add-modal/driveitem-folder-add-modal.component";
import {
    DriveItemFileAddModalComponent
} from "./drive/modals/additions/driveitem-file-add-modal/driveitem-file-add-modal.component";

// App-Modal-Update
import { ApplicationRoleUpdateModalComponent }
  from './management/modals/updates/applicationrole-update-modal/applicationrole-update-modal.component';
import { ApplicationUserUpdateModalComponent }
  from './management/modals/updates/applicationuser-update-modal/applicationuser-update-modal.component';
import {
    DriveitemNameUpdateModalComponent
} from "./drive/modals/updates/driveitem-name-update-modal/driveitem-name-update-modal.component";
import {
    DriveitemShareWithUpdateModalComponent
} from "./drive/modals/updates/driveitem-share-with-update-modal/driveitem-share-with-update-modal.component";

// App-Drive-Context
import { DriveItemContextMenuComponent } from './drive/menus/drive-item-context-menu/drive-item-context-menu.component';
import { DriveItemNewMenuComponent} from "./drive/menus/drive-item-new-menu/drive-item-new-menu.component";

@NgModule({
  declarations: [
    // App
    AppComponent,
    HomeComponent,
    UnknownComponent,
    UnauthorizedComponent,
    //Nav
    NavMenuComponent,
    ProfileContextMenuComponent,
    ToolboxContextMenuComponent,
    // App-Auth
    JoinInComponent,
    SignInComponent,
    ResetPasswordComponent,
    // App-Security
    SecurityComponent,
    ChangePasswordModalComponent,
    ChangeEmailModalComponent,
    ChangePhoneNumberModalComponent,
    ChangeNameModalComponent,
    // App-Grid
    ApplicationRoleGridComponent,
    ApplicationUserGridComponent,
    DriveitemGridComponent,
    // App-Modal-Adition
    ApplicationRoleAddModalComponent,
    DriveItemFileAddModalComponent,
    DriveItemFolderAddModalComponent,
    // App-Modal-Update
    ApplicationRoleUpdateModalComponent,
    ApplicationUserUpdateModalComponent,
    DriveitemNameUpdateModalComponent,
    DriveitemShareWithUpdateModalComponent,
    // App-Drive-Context
    DriveItemContextMenuComponent,
    DriveItemNewMenuComponent
  ],
  bootstrap: [AppComponent], imports: [
    // Angular Material
    BrowserAnimationsModule,
    MatDividerModule,
    MatSelectModule,
    MatInputModule,
    MatDialogModule,
    MatPaginatorModule,
    MatButtonModule,
    MatBottomSheetModule,
    MatListModule,
    MatSnackBarModule,
    MatChipsModule,
    MatAutocompleteModule,
    MatCardModule,
    MatTableModule,
    MatTabsModule,
    MatSortModule,
    MatFormFieldModule,
    MatExpansionModule,
    MatToolbarModule,
    MatSidenavModule,
    MatMenuModule,
    MatTooltipModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    NgOptimizedImage], providers: [{
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true,
    }, provideHttpClient(withInterceptorsFromDi())]
})
export class AppModule { }
