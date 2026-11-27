import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withAutoCleanupInjectors } from '@angular/router';
import {
  provideHttpClient,
  withInterceptors,
  withXhr,
} from '@angular/common/http';

import { routes } from './app.routes';
import { AuthInterceptor } from '../interceptors/auth.interceptor';

export const AppConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: false }),
    provideRouter(routes, withAutoCleanupInjectors()),
    provideHttpClient(withXhr(), withInterceptors([AuthInterceptor])),
  ],
};
