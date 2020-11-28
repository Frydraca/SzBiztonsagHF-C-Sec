import {UserDataResponse} from './user-data-response';

export class User {
  id: string;
  userName: string;

  constructor() {
  }

  public setData(userDataResponse: UserDataResponse): void {
    this.id = userDataResponse.id;
    this.userName = userDataResponse.userName;
  }

}
