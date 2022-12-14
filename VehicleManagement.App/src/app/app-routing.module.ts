import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BookingOverviewComponent } from './bookings/booking-overview/booking-overview.component';
import { DashboardComponent } from './dashboard/dashboard/dashboard.component';
import { VehicleOverviewComponent } from './vehicles/vehicle-overview/vehicle-overview.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
  },
  {
    path: 'vehicles',
    component: VehicleOverviewComponent,
  },
  {
    path: 'bookings',
    component: BookingOverviewComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
