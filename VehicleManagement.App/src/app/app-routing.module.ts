import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddBookingComponent } from './bookings/add-booking/add-booking.component';
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
  {
    path: 'bookings/add',
    component: AddBookingComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
