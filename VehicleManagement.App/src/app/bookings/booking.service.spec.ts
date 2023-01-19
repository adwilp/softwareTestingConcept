import { HttpErrorResponse } from '@angular/common/http';
import {
  HttpClientTestingModule,
  HttpTestingController,
  TestRequest,
} from '@angular/common/http/testing';
import { TestBed } from '@angular/core/testing';
import { environment } from 'src/environments/environment';
import { BookingService } from './booking.service';
import { FlatBooking } from './models/flat-booking.model';
import { UpdateableBooking } from './models/updateable-booking.model';
import {
  booking,
  flatBookings,
  updateableBooking,
} from './_store/booking.test-data';

const baseUrl: string = environment.baseUrl;

describe('BookingService', () => {
  let bookingService: BookingService;
  let controller: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [BookingService],
    });

    bookingService = TestBed.inject(BookingService);
    controller = TestBed.inject(HttpTestingController);
  });

  it('loads all bookings', () => {
    // ARRANGE
    let currentBookings: FlatBooking[] | undefined;
    bookingService.getBookings().subscribe((bookings: FlatBooking[]) => {
      currentBookings = bookings;
    });

    // ACT
    const request: TestRequest = controller.expectOne({
      method: 'GET',
      url: `${baseUrl}Bookings`,
    });
    request.flush(flatBookings);
    controller.verify();

    // ASSERT
    expect(currentBookings).toEqual(flatBookings);
  });

  it('passes through all bookings errors on get all', () => {
    // ARRANGE
    const status: number = 500;
    const statusText: string = 'Server error';
    const progressEvent: ProgressEvent = new ProgressEvent('API Error');

    let currentError: HttpErrorResponse | undefined;

    bookingService.getBookings().subscribe(
      () => {
        fail('next handler must not be called!');
      },
      (error: HttpErrorResponse) => {
        currentError = error;
      },
      () => {
        fail('complete handler must not be called!');
      }
    );

    // ACT
    const request: TestRequest = controller.expectOne({
      method: 'GET',
      url: `${baseUrl}Bookings`,
    });
    request.error(progressEvent, { status, statusText });

    // ASSERT
    if (!currentError) {
      fail('error response must not be undefined!');
    }

    expect(currentError.error).toBe(progressEvent);
    expect(currentError.status).toBe(status);
    expect(currentError.statusText).toBe(statusText);
  });

  it('adds booking', () => {
    // ARRANGE
    let currentBooking: FlatBooking | undefined;
    bookingService.addBooking(booking).subscribe((flatBooking: FlatBooking) => {
      currentBooking = flatBooking;
    });

    // ACT
    const request: TestRequest = controller.expectOne({
      method: 'POST',
      url: `${baseUrl}Bookings`,
    });
    request.flush(flatBookings[0]);
    controller.verify();

    // ASSERT
    expect(request.request.body).toEqual(booking);
    expect(currentBooking).toEqual(flatBookings[0]);
  });

  it('passes through all bookings errors on add', () => {
    // ARRANGE
    const status: number = 500;
    const statusText: string = 'Server error';
    const progressEvent: ProgressEvent = new ProgressEvent('API Error');

    let currentError: HttpErrorResponse | undefined;

    bookingService.addBooking(booking).subscribe(
      () => {
        fail('next handler must not be called!');
      },
      (error: HttpErrorResponse) => {
        currentError = error;
      },
      () => {
        fail('complete handler must not be called!');
      }
    );

    // ACT
    const request: TestRequest = controller.expectOne({
      method: 'POST',
      url: `${baseUrl}Bookings`,
    });
    request.error(progressEvent, { status, statusText });

    // ASSERT
    if (!currentError) {
      fail('error response must not be undefined!');
    }

    expect(currentError.error).toBe(progressEvent);
    expect(currentError.status).toBe(status);
    expect(currentError.statusText).toBe(statusText);
  });

  it('edits booking', () => {
    // ARRANGE
    let currentBooking: FlatBooking | undefined;
    bookingService
      .editBooking(updateableBooking)
      .subscribe((flatBooking: FlatBooking) => {
        currentBooking = flatBooking;
      });

    // ACT
    const request: TestRequest = controller.expectOne({
      method: 'PUT',
      url: `${baseUrl}Bookings`,
    });
    request.flush(flatBookings[0]);
    controller.verify();

    // ASSERT
    expect(request.request.body).toEqual(updateableBooking);
    expect(currentBooking).toEqual(flatBookings[0]);
  });

  it('passes through all bookings errors on edit', () => {
    // ARRANGE
    const status: number = 500;
    const statusText: string = 'Server error';
    const progressEvent: ProgressEvent = new ProgressEvent('API Error');

    let currentError: HttpErrorResponse | undefined;

    bookingService.editBooking(updateableBooking).subscribe(
      () => {
        fail('next handler must not be called!');
      },
      (error: HttpErrorResponse) => {
        currentError = error;
      },
      () => {
        fail('complete handler must not be called!');
      }
    );

    // ACT
    const request: TestRequest = controller.expectOne({
      method: 'PUT',
      url: `${baseUrl}Bookings`,
    });
    request.error(progressEvent, { status, statusText });

    // ASSERT
    if (!currentError) {
      fail('error response must not be undefined!');
    }

    expect(currentError.error).toBe(progressEvent);
    expect(currentError.status).toBe(status);
    expect(currentError.statusText).toBe(statusText);
  });

  it('loads booking', () => {
    // ARRANGE
    let currentBooking: UpdateableBooking | undefined;
    bookingService.getBooking(1).subscribe((booking: UpdateableBooking) => {
      currentBooking = booking;
    });

    // ACT
    const request: TestRequest = controller.expectOne({
      method: 'GET',
      url: `${baseUrl}Bookings/${1}`,
    });
    request.flush(updateableBooking);
    controller.verify();

    // ASSERT
    expect(currentBooking).toEqual(updateableBooking);
  });

  it('passes through all bookings errors on get single', () => {
    // ARRANGE
    const status: number = 500;
    const statusText: string = 'Server error';
    const progressEvent: ProgressEvent = new ProgressEvent('API Error');

    let currentError: HttpErrorResponse | undefined;

    bookingService.getBooking(1).subscribe(
      () => {
        fail('next handler must not be called!');
      },
      (error: HttpErrorResponse) => {
        currentError = error;
      },
      () => {
        fail('complete handler must not be called!');
      }
    );

    // ACT
    const request: TestRequest = controller.expectOne({
      method: 'GET',
      url: `${baseUrl}Bookings/${1}`,
    });
    request.error(progressEvent, { status, statusText });

    // ASSERT
    if (!currentError) {
      fail('error response must not be undefined!');
    }

    expect(currentError.error).toBe(progressEvent);
    expect(currentError.status).toBe(status);
    expect(currentError.statusText).toBe(statusText);
  });
});
