import { Routes } from "@angular/router";
import { SignInGuard } from "src/guards/signin.guard";

export const routes: Routes = [
    {
        path: '',
        loadComponent: () => import('./home/home.component').then((m) => m.HomeComponent),
        pathMatch: 'full',
        canActivate: [SignInGuard]
    },
    // App-Auth
    {
        path: 'auth/joinin',
        loadComponent: () => import('./auth/joinin/joinin.component').then((m) => m.JoinInComponent),
        pathMatch: 'full'
    },
    {
        path: 'auth/signin',
        loadComponent: () => import('./auth/signin/signin.component').then((m) => m.SignInComponent),
        pathMatch: 'full'
    },
    {
        path: 'auth/resetpassword',
        loadComponent: () => import('./auth/resetpassword/resetpassword.component').then((m) => m.ResetPasswordComponent),
        pathMatch: 'full'
    },
    // App-Security
    {
        path: 'security',
        loadComponent: () => import('./security/security.component').then((m) => m.SecurityComponent),
        pathMatch: 'full',
        canActivate: [SignInGuard]
    },
    // App-Management
    {
        path: 'management/applicationrole',
        loadComponent: () => import('./management/grids/applicationrole-grid/applicationrole-grid.component').then((m) => m.ApplicationRoleGridComponent),
        pathMatch: 'full',
        canActivate: [SignInGuard]
    },
    {
        path: 'management/applicationuser',
        loadComponent: () => import('./management/grids/applicationuser-grid/applicationuser-grid.component').then((m) => m.ApplicationUserGridComponent),
        pathMatch: 'full',
        canActivate: [SignInGuard]
    },
    {
        path: 'unknown',
        loadComponent: () => import('./unknown/unknown.component').then((m) => m.UnknownComponent),
        pathMatch: 'full'
    },
    {
        path: 'unauthorized',
        loadComponent: () => import('./unauthorized/unauthorized.component').then((m) => m.UnauthorizedComponent),
        pathMatch: 'full'
    }
];