import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-unknown',
    templateUrl: './unknown.component.html',
    styleUrl: './unknown.component.scss',
    standalone: false
})
export class UnknownComponent {

  // Constructor
  constructor(private router: Router) { }

  public async Back(): Promise<void> {
    await this.router.navigate([""]);
  }
}
