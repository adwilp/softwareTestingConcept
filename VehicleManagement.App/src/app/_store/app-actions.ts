/* eslint-disable @typescript-eslint/typedef */
import { HttpErrorResponse } from '@angular/common/http';
import { createAction, props } from '@ngrx/store';

export type appError = { message: string; error: HttpErrorResponse };
export const appError = createAction('[App] Error occured', props<appError>());
