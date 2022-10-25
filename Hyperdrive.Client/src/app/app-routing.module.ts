import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SignInGuard } from '../guards/signin.guard';
import { JoinInAuthComponent } from './auth/joinin-auth/joinin-auth.component';
import { SignInAuthComponent } from './auth/signin-auth/signin-auth.component';
import { HomeComponent } from './home/home.component';
import { ArchiveGridComponent } from './management/grids/archive-grid/archive-grid.component';
import { ArchiveSharedGridComponent } from './management/grids/archive-shared-grid/archive-shared-grid.component';
import { ChangeEmailSecurityComponent } from './security/changeemail-security/changeemail-security.component';
import { ChangePasswordSecurityComponent } from './security/changepassword-security/changepassword-security.component';
import { ResetPasswordSecurityComponent } from './security/resetpassword-security/resetpassword-security.component';

@NgModule({
  imports: [RouterModule.forRoot([
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
  exports: [RouterModule]
})
export class AppRoutingModule { }
