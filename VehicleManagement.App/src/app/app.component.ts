import { Component, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material/sidenav';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  @ViewChild('sidenav') sidenav: MatSidenav;

  constructor(private router: Router) {}

  navigateToHome(): void {
    this.router.navigate(['']);
  }

  navigateToVehicles(): void {
    this.router.navigate(['vehicles']);
  }

  navigateToBookings(): void {
    this.router.navigate(['bookings']);
  }
}
