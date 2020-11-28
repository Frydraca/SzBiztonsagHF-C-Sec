import {Inject, Injectable} from '@angular/core';
import {HttpClient, HttpErrorResponse, HttpHeaders} from '@angular/common/http';
import {LoginData} from '../models/login-data';
import {Observable, throwError} from 'rxjs';
import {LoginResponse} from '../models/login-response';
import {UserDataResponse} from '../models/user-data-response';
import {RegisterData} from '../models/register-data';
import {RegisterResponse} from '../models/register-response';
import {User} from '../models/user';
import {map} from 'rxjs/operators';
import {CaffImage} from '../models/caff-image';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  private readonly baseUrl: string;
  private header;
  private token: string;
  private tokenExpirationTime: number;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private httpClient: HttpClient) {
    this.baseUrl = baseUrl;
  }

  public setHeader(token: string, tokenExpirationTime: number): void {
    this.token = token;
    this.tokenExpirationTime = tokenExpirationTime;
    this.header = {
      Authorization: 'Bearer ' + token,
    };

    this.sendKeepAlive();
  }

  public register(registerData: RegisterData): Observable<RegisterResponse> {
    return this.postRequest<RegisterResponse>('auth/register', registerData);
  }

  public logout(): Observable<void> {
    const logoutObservable = this.postRequest<void>('auth/logout', null);

    this.clearToken();

    return logoutObservable;
  }

  public login(loginData: LoginData): Observable<LoginResponse> {
    return this.postRequest<LoginResponse>('auth/login', loginData).pipe(
      map(
        loginResponse => this.handleLoginResponse(loginResponse),
        error => this.handleLoginError(error)
      ));
  }

  public addComment(imageId: string, comment: { text: string }): Observable<any> {
    return this.postRequest<any>(`caff/${imageId}/comments`, comment);
  }

  public deleteComment(imageId: string, commentId: string): Observable<any> {
    return this.deleteRequest<any>(imageId + '/comments/' + commentId);
  }

  public getImageById(imageId: string): Observable<CaffImage> {
    return this.getRequest<CaffImage>('caff/' + imageId);
  }

  public getUserData(): Observable<UserDataResponse> {
    return this.getRequest<UserDataResponse>('usermanagement');
  }

  public getAllUser(): Observable<any> {
    return this.getRequest<any>('usermanagement/all');
  }

  public deleteUser(user: User): Observable<any>{
    return this.deleteRequestWithBody<any>('usermanagement', user);
  }

  private refreshJwt(): Observable<LoginResponse> {
    return this.postRequest<LoginResponse>('auth/refresh-jwt', null);
  }

  private getRequest<T>(urlEnd: string): Observable<T> {
    return this.httpClient.get<T>(this.baseUrl + urlEnd, this.getHeader());
  }

  private deleteRequest<T>(urlEnd: string): Observable<T> {
    return this.httpClient.delete<T>(this.baseUrl + urlEnd, this.getHeader());
  }

  private deleteRequestWithBody<T>(urlEnd: string, postData: any): Observable<T> {
    return this.httpClient.request<T>('delete', this.baseUrl + urlEnd,
     { body: postData, headers: this.getHeader().headers  });
  }

  private postRequest<T>(urlEnd: string, postData: any): Observable<T> {
    return this.httpClient.post<T>(this.baseUrl + urlEnd, postData, this.getHeader());
  }

  private putRequest<T>(urlEnd: string, postData: any): Observable<T> {
    return this.httpClient.put<T>(this.baseUrl + urlEnd, postData, this.getHeader());
  }

  private getHeader(): { headers: HttpHeaders } {
    return {
      headers: this.header
    };
  }

  private handleLoginResponse(loginResponse: LoginResponse): LoginResponse {
    this.setHeader(loginResponse.token, loginResponse.tokenExpirationTime);
    return loginResponse;
  }

  private sendKeepAlive(): void {
    const tokenRefreshOffset = (this.tokenExpirationTime - 2) * 1000;
    setTimeout(() => this.keepAlive(), tokenRefreshOffset);
  }

  private keepAlive(): void {
    if (this.token && this.token.length > 0) {
      this.refreshJwt().subscribe(
        refreshResponse => this.handleLoginResponse(refreshResponse),
        error => this.handleJwtError(error)
      );
    }
  }

  private handleJwtError(error: HttpErrorResponse): void {
    this.clearToken();
  }

  private handleLoginError(error: HttpErrorResponse): void {
    this.handleJwtError(error);
    throwError(error);
  }

  private clearToken(): void {
    this.header = null;
    this.token = null;
    this.tokenExpirationTime = null;
  }
}
