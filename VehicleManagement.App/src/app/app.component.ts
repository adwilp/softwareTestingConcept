import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {

  constructor(private router: Router) {}

  navigateToHome(): void {
    this.router.navigate(['']);
  }

  navigateToVehicles(): void {
    this.router.navigate(['vehicles']);
  }
}
