import {Injectable} from '@angular/core';
import {User} from './models/user';
import {UserDataResponse} from './models/user-data-response';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private loggedInUser: User;
  private isUserLoggedIn = false;

  constructor() {
    this.loggedInUser = new User();
  }

  public setLoggedInUser(userData: UserDataResponse): void {
    this.loggedInUser.setData(userData);
    this.isUserLoggedIn = true;
  }

  public logout(): void {
    this.loggedInUser = new User();
    this.isUserLoggedIn = false;
  }
}
