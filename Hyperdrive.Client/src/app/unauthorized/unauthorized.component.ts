import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrl: './unauthorized.component.scss'
})
export class UnauthorizedComponent {
  // Constructor
  constructor(private router: Router) { }

  public Back(): void {
    this.router.navigate([""]);
  }
}
