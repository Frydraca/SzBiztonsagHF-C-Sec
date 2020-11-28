import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {LoginData} from './models/login-data';
import {Observable} from 'rxjs';
import {LoginResponse} from './models/login-response';
import {UserDataResponse} from './models/user-data-response';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private readonly baseUrl: string;
  private header;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private httpClient: HttpClient) {
    this.baseUrl = baseUrl;
  }

  public setHeader(token: string): void {
    this.header = {
      Authorization: 'Bearer ' + token,
    };
  }

  public login(loginData: LoginData): Observable<LoginResponse> {
    return this.postRequest<LoginResponse>('auth/login', loginData);
  }

  public getUserData(): Observable<UserDataResponse> {
    return this.getRequest<UserDataResponse>('usermanagement');
  }

  public getAllUser(): Observable<any> {
    return this.getRequest<any>('usermanagement/all');
  }

  private getRequest<T>(urlEnd: string): Observable<T> {
    return this.httpClient.get<T>(this.baseUrl + urlEnd, this.getHeader());
  }

  private postRequest<T>(urlEnd: string, postData: any): Observable<T> {
    return this.httpClient.post<T>(this.baseUrl + urlEnd, postData, this.getHeader());
  }

  private getHeader(): { headers: HttpHeaders } {
    return {
      headers: this.header
    };
  }
}
