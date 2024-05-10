import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SignInGuard } from '../guards/signin.guard';
import { JoinInAuthComponent } from './auth/joinin-auth/joinin-auth.component';
import { SignInAuthComponent } from './auth/signin-auth/signin-auth.component';
import { HomeComponent } from './home/home.component';
import { ChangeEmailSecurityComponent } from './security/changeemail-security/changeemail-security.component';
import { ChangePasswordSecurityComponent } from './security/changepassword-security/changepassword-security.component';
import { ResetPasswordAuthComponent } from './auth/resetpassword-auth/resetpassword-auth.component';
import { UnknownComponent } from './unknown/unknown.component';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { ApplicationRoleGridComponent } from './management/grids/applicationrole-grid/applicationrole-grid.component';
import { ApplicationUserGridComponent } from './management/grids/applicationuser-grid/applicationuser-grid.component';
import { SecurityComponent } from './security/security.component'

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
    {
      path: 'auth/resetpassword',
      component: ResetPasswordAuthComponent,
      pathMatch: 'full'
    },
    // App-Security
    {
      path: 'security',
      component: SecurityComponent,
      pathMatch: 'full',
      canActivate: [SignInGuard]
    },
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
    // App-Management
    {
      path: 'management/applicationrole',
      component: ApplicationRoleGridComponent,
      pathMatch: 'full',
      canActivate: [SignInGuard]
    },
    {
      path: 'management/applicationuser',
      component: ApplicationUserGridComponent,
      pathMatch: 'full',
      canActivate: [SignInGuard]
    },
    {
      path: 'unknown',
      component: UnknownComponent,
      pathMatch: 'full'
    },
    {
      path: 'unauthorized',
      component: UnauthorizedComponent,
      pathMatch: 'full'
    }
  ])
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
