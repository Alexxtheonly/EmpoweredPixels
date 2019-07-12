import { Login } from './+models/login';
import { HttpClient } from '@angular/common/http';
import { Token } from './+models/token';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private jwtHelper: JwtHelperService;

  constructor(private http: HttpClient) {
  }

  public isAuthenticated(): boolean {
    const token = localStorage.getItem('token');

    return !this.jwtHelper.isTokenExpired(token);
  }

  public authenticate(login: Login): Observable<Token> {
    return this.http.post<Token>('api/authentication/token', login);
  }

  public authenticateFor(user: string, password: string): Observable<Token> {
    const login = new Login();
    login.username = user;
    login.password = password;

    return this.authenticate(login);
  }
}
