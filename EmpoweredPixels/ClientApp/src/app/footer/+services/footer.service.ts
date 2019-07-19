import { VersionInformation } from './../+models/version-information';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class FooterService {

  constructor(private http: HttpClient) { }

  public getVersionInformation(): Observable<VersionInformation> {
    return this.http.get<VersionInformation>('api/information/version');
  }
}
