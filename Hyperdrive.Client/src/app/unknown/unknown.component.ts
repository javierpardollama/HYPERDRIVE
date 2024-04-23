import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-unknown',
  templateUrl: './unknown.component.html',
  styleUrl: './unknown.component.scss'
})
export class UnknownComponent {

  // Constructor
  constructor(private router: Router) { }

  public Back(): void {
    this.router.navigate([""]);
  }
}
