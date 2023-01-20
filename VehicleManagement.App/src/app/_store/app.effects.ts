import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { tap } from 'rxjs';
import * as AppActions from './app-actions';

@Injectable()
export class AppEffects {
  //eslint-disable-next-line @typescript-eslint/typedef
  appError$ = createEffect(
    () => {
      return this.action$.pipe(
        ofType(AppActions.appError),
        tap((action: AppActions.appError) => {
          console.error(action.message);
          console.dir(action.error);
        })
      );
    },
    { dispatch: false }
  );

  constructor(private action$: Actions) {}
}
