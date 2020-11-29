import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service'; //get logged in user + is admin
import { AdminService } from '../../services/admin.service'; //get user for admin
import { HttpService } from '../../services/http.service'; //update calls
import { MessageService } from '../../services/message.service'; //show result
import { User } from 'src/app/models/user';
import { ChangePasswordData } from 'src/app/models/change-password-data';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  public routeParam: string;
  private isUserAdmin;
  private user: User;
  public usernameFormGroup: FormGroup;
  public passwordFormGroup: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private adminService: AdminService,
    private httpService: HttpService,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.isUserAdmin = this.userService.isAdmin();
    this.initForms();
    this.getUser();
  }

  private getUser(): void {
    console.log(this.isUserAdmin);
    if (this.isUserAdmin) {
      this.getUserForAdmin();
    } else {
      this.user = this.userService.getLoggedInUser();
      this.setUserName();
    }
  }

  private getUserForAdmin(): void {
    this.route.paramMap.subscribe(paramMap => {
      console.log(paramMap.get('id'));
      this.routeParam = paramMap.get('id');
      this.user = this.adminService.getUserById(this.routeParam);
      this.setUserName();
    });
  }

  public onUpdateUsername(): void {
    console.log({ ...this.user, ...this.usernameFormGroup.getRawValue() });
    this.httpService
      .updateUser({ ...this.user, ...this.usernameFormGroup.getRawValue() })
      .subscribe(
        respone => this.handleResponse(respone),
        error => this.handleError(error)
      );
  }

  public onUpdatePassword(): void {
    console.log({ ...this.user, ...this.passwordFormGroup.getRawValue() });
    this.httpService
      .changePassword({ ...this.user, ...this.passwordFormGroup.getRawValue() })
      .subscribe(
        respone => this.handleResponse(respone),
        error => this.handleError(error)
      );
  }

  private handleResponse(result: any) {
    this.messageService.showInfoMessage('Successfull');
    console.log(result);
  }

  private handleError(error: any) {
    this.messageService.showErrorMessage('Failed');
    console.log(error);
  }

  private initForms(): void {
    this.usernameFormGroup = new FormGroup({
      userName: new FormControl(null, Validators.required)
    });
    this.passwordFormGroup = new FormGroup({
      oldPassword: new FormControl(null, Validators.required),
      newPassword: new FormControl(null, Validators.required),
      repeatedNewPassword: new FormControl(null, Validators.required)
    });
  }

  private setUserName(): void {
    this.usernameFormGroup.setValue({ userName: this.user.userName });
  }
}
