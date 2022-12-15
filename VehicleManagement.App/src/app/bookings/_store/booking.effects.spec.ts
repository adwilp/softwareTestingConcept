import { TestBed } from '@angular/core/testing';
import { Observable, of, throwError } from 'rxjs';
import { provideMockActions } from '@ngrx/effects/testing';
import { BookingEffects } from './booking.effects';
import { bookingReducer } from './booking.reducers';
import { BookingService } from '../booking.service';
import { MockProviders } from 'ng-mocks';
import * as BookingActions from './booking.actions';
import Spy = jasmine.Spy;
import { HttpErrorResponse } from '@angular/common/http';
import { flatBookings } from './booking.test-data';
import { TypedAction } from '@ngrx/store/src/models';

describe('BookingEffects', () => {
  type getBookingsSuccessAction = BookingActions.getBookingsSuccess &
    TypedAction<'[Bookings] Get bookings - Success'>;

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  let actionsMock$: Observable<any>;
  let effects: BookingEffects;
  let bookingService: BookingService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        provideMockActions(() => actionsMock$),
        BookingEffects,
        MockProviders(bookingReducer, BookingService),
      ],
    });

    bookingService = TestBed.inject(BookingService);
    effects = TestBed.inject(BookingEffects);
  });

  describe('getBookings$', () => {
    let bookingSpy: Spy;

    beforeEach(() => {
      actionsMock$ = of(BookingActions.getBookings);
      bookingSpy = spyOn(bookingService, 'getBookings');
    });

    it('calls getBookings once', () => {
      // ARRANGE
      bookingSpy.and.returnValue(of(flatBookings));

      // ACT
      effects.getBookings$.subscribe();

      // ASSERT
      expect(bookingSpy).toHaveBeenCalledOnceWith();
    });

    it('returns a stream with FlatBookings', () => {
      // ARRANGE
      let currentOutcome: getBookingsSuccessAction | undefined;
      const outcome: getBookingsSuccessAction =
        BookingActions.getBookingsSuccess({
          bookings: flatBookings,
        });
      bookingSpy.and.returnValue(of(flatBookings));

      // ACT
      effects.getBookings$.subscribe((action: getBookingsSuccessAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        bookings: outcome.bookings,
      });
    });

    it('returns a stream with null on http error', () => {
      // ARRANGE
      let currentOutcome: getBookingsSuccessAction | undefined;
      const error: HttpErrorResponse = new HttpErrorResponse({});
      const outcome: getBookingsSuccessAction =
        BookingActions.getBookingsSuccess({ bookings: null });
      bookingSpy.and.callFake(() => {
        return throwError(() => error);
      });

      // ACT
      effects.getBookings$.subscribe((action: getBookingsSuccessAction) => {
        currentOutcome = action;
      });

      // ASSERT
      if (!currentOutcome) {
        fail('currentOutcome must not be undefined!');
      }

      expect(currentOutcome).toEqual({
        type: outcome.type,
        bookings: outcome.bookings,
      });
    });
  });
});
