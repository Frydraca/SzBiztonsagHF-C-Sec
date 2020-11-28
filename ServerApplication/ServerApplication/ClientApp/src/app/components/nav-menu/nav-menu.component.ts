import {Component} from '@angular/core';
import {HttpErrorResponse} from '@angular/common/http';
import {HttpService} from '../../services/http.service';
import {UserService} from '../../services/user.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(
    private httpService: HttpService,
    private userService: UserService) {
  }


  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  public logout(): void {
    this.userService.logout();
    this.httpService.logout().subscribe(
      () => this.handleLogoutResponse(),
      error => this.handleLogoutError(error)
    );
  }

  private handleLogoutResponse(): void {

  }

  private handleLogoutError(error: HttpErrorResponse): void {

  }
}
