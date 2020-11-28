import {Injectable} from '@angular/core';
import {User} from '../models/user';
import {UserDataResponse} from '../models/user-data-response';
import {HttpService} from './http.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private loggedInUser: User;
  private isUserLoggedIn = false;
  private isUserAdmin = false;

  constructor(private httpService: HttpService) {
    this.loggedInUser = new User();
  }

  public setRole(isAdmin: boolean): void {
    if (isAdmin) {
      this.isUserAdmin = true;
    }
  }

  public isAuthenticated(): boolean {
    return this.isUserLoggedIn;
  }

  public isAdmin(): boolean {
    return this.isUserAdmin;
  }

  public setLoggedInUser(userData: UserDataResponse): void {
    this.loggedInUser.setData(userData);
    this.isUserLoggedIn = true;
  }

  public logout(): void {
    this.loggedInUser = new User();
    this.isUserLoggedIn = false;
    this.isUserAdmin = false;
  }
}
