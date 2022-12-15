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
    this.navigate(['']);
  }

  navigateToVehicles(): void {
    this.navigate(['vehicles']);
  }

  navigateToBookings(): void {
    this.navigate(['bookings']);
  }

  private navigate(path: string[]): void {
    this.sidenav.close();
    this.router.navigate(path);
  }
}
