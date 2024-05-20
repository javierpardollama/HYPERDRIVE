import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SignInGuard } from '../guards/signin.guard';
import { JoinInComponent } from './auth/joinin/joinin.component';
import { SignInComponent } from './auth/signin/signin.component';
import { HomeComponent } from './home/home.component';
import { ResetPasswordComponent } from './auth/resetpassword/resetpassword.component';
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
      component: JoinInComponent,
      pathMatch: 'full'
    },
    {
      path: 'auth/signin',
      component: SignInComponent,
      pathMatch: 'full'
    },
    {
      path: 'auth/resetpassword',
      component: ResetPasswordComponent,
      pathMatch: 'full'
    },
    // App-Security
    {
      path: 'security',
      component: SecurityComponent,
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
