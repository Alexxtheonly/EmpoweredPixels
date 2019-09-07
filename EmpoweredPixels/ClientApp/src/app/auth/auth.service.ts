import { UserFeedbackService } from '../+services/userfeedback.service';
import { Login } from './+models/login';
import { HttpClient } from '@angular/common/http';
import { Token } from './+models/token';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService
{

  private jwtHelper: JwtHelperService = new JwtHelperService();

  constructor(private http: HttpClient)
  {
  }

  public isAuthenticated(): boolean
  {
    const token = this.getToken();
    if (!token)
    {
      return false;
    }

    const jwt = token.token;

    return !this.jwtHelper.isTokenExpired(jwt);
  }

  public login(login: Login): Observable<Token>
  {
    return this.http.post<Token>('api/authentication/token', login).pipe(map(token =>
    {
      localStorage.setItem('token', JSON.stringify(token));

      return token;
    }));
  }

  public logout()
  {
    localStorage.removeItem('token');
  }

  public getJwtToken(): any
  {
    const token = this.getToken();
    if (!token)
    {
      return;
    }

    return this.jwtHelper.decodeToken(token.token);
  }

  public getUserId(): number
  {
    const jwt = this.getJwtToken();

    if (!jwt)
    {
      return;
    }

    return Number(jwt.unique_name);
  }

  private getToken(): Token
  {
    const json = JSON.parse(localStorage.getItem('token'));
    const token: Token = json;

    return token;
  }
}
