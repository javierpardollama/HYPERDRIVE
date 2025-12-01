import { Component } from '@angular/core';
import { Meta } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { environment } from 'src/environments/environment';
import { NavMenuComponent } from './shell/menus/nav-menu/nav-menu.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  imports: [
    RouterModule,
    NavMenuComponent
  ]
})
export class AppComponent {
  constructor(private meta: Meta) {
    this.ApplyContenSecurityPolicy();
  }

  ApplyContenSecurityPolicy(): void {
    const content = [
      "default-src 'self'",
      "style-src 'self' 'unsafe-inline'",
      "script-src 'self'",
      "img-src 'self' data:",
      `connect-src 'self' ${environment.Api.Service} ${environment.Otel.Exporter}`
    ].join("; ");

    this.meta.addTag({ 'http-equiv': 'Content-Security-Policy', content });
  }
}
