import { Routes } from "@angular/router";
import { AuthGuard } from "../guards/auth.guard";
import { GuestGuard } from "../guards/guest.guard";

export const routes: Routes = [
  // 🔒 Authenticated Routes
  {
    path: '',
    canActivate: [AuthGuard],
    children: [
      {
        path: '',
        loadComponent: () => import('./home/home.component').then((m) => m.HomeComponent),
        pathMatch: 'full'
      },
      {
        path: 'security',
        loadComponent: () => import('./security/security.component').then((m) => m.SecurityComponent),
        pathMatch: 'full'
      },
      {
        path: 'management/applicationrole',
        loadComponent: () => import('./management/grids/applicationrole-grid/applicationrole-grid.component').then((m) => m.ApplicationRoleGridComponent),
        pathMatch: 'full'
      },
      {
        path: 'management/applicationuser',
        loadComponent: () => import('./management/grids/applicationuser-grid/applicationuser-grid.component').then((m) => m.ApplicationUserGridComponent),
        pathMatch: 'full'
      }
    ]
  },
  // 🔓 Guest Routes
  {
    path: 'auth',
    canActivate: [GuestGuard],
    children: [
      {
        path: '',
        redirectTo: 'signin',
        pathMatch: 'full'
      },
      {
        path: 'joinin',
        loadComponent: () => import('./auth/joinin/joinin.component').then((m) => m.JoinInComponent),
        pathMatch: 'full'
      },
      {
        path: 'signin',
        loadComponent: () => import('./auth/signin/signin.component').then((m) => m.SignInComponent),
        pathMatch: 'full'
      },
      {
        path: 'resetpassword',
        loadComponent: () => import('./auth/resetpassword/resetpassword.component').then((m) => m.ResetPasswordComponent),
        pathMatch: 'full'
      }
    ]
  },
  // 🗺️ Public / Error Routes
  {
    path: 'unknown',
    loadComponent: () => import('./unknown/unknown.component').then((m) => m.UnknownComponent),
    pathMatch: 'full'
  },
  {
    path: 'unauthorized',
    loadComponent: () => import('./unauthorized/unauthorized.component').then((m) => m.UnauthorizedComponent),
    pathMatch: 'full'
  },
  // 🚨 Wildcard Fallback
  {
    path: '**',
    redirectTo: 'unknown'
  }
];
