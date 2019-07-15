import { catchError, map } from 'rxjs/operators';
import { HttpRequest, HttpErrorResponse, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { throwError, Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(this.addTokenToRequest(request)).pipe(
            map(res => res),
            catchError(err => {
                if (err instanceof HttpErrorResponse && err.status === 401 && err.headers.has('Token-Expired')) {
                    // here code to refresh token if needed
                } else {
                    return throwError(err);
                }
            })
        );
    }

    private addTokenToRequest(request: HttpRequest<any>, token: string = null): HttpRequest<any> {
        if (token) {
            request = request.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
        } else {
            const tokenJson = JSON.parse(localStorage.getItem('token'));
            if (tokenJson && tokenJson.token) {
                request = request.clone({
                    setHeaders: {
                        'Authorization': `Bearer ${tokenJson.token}`,
                        'Content-Type': 'application/json'
                    }
                });
            }
        }
        return request;
    }
}
