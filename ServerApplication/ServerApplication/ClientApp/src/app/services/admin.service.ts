import {Injectable} from '@angular/core';
import {User} from '../models/user';
import {Subject} from 'rxjs';
import {HttpService} from './http.service';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  public userListChanged: Subject<void> = new Subject();
  private users: User[] = [];

  constructor(private httpService: HttpService) {
  }

  public refreshList(): void {
    this.httpService.getAllUser().subscribe(
      users => this.handleUserListResponse(users),
      error => console.error(error)
    );
  }

  public getAllUsers(): User[] {
    return this.users;
  }

  public getUserById(userId: string): User {
    return this.users.find(user => user.id === userId);
  }

  private handleUserListResponse(users: User[]): void {
    this.users = users;
    this.userListChanged.next();
  }
}
