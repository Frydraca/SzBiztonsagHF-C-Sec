import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {HttpService} from '../../http.service';
import {LoginResponse} from '../../models/login-response';
import {HttpErrorResponse} from '@angular/common/http';
import {UserDataResponse} from '../../models/user-data-response';
import {UserService} from '../../user.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  public loginFormGroup: FormGroup;

  constructor(
    private userService: UserService,
    private httpService: HttpService) {
  }

  ngOnInit() {
    this.initForm();
  }

  public onSubmit(): void {
    if (this.loginFormGroup.valid) {
      this.httpService.login(this.loginFormGroup.getRawValue())
        .subscribe(
          loginResponse => this.handleLoginResponse(loginResponse),
          error => this.handleLoginError(error)
        );
    } else {
      this.loginFormGroup.markAllAsTouched();
    }
  }

  private handleLoginResponse(loginResponse: LoginResponse): void {
    this.httpService.setHeader(loginResponse.token);
    this.httpService.getUserData().subscribe(
      userDataResponse => this.handleGetUserResponse(userDataResponse),
      error => this.handleGetUserError(error)
    );
  }

  private handleLoginError(loginError: HttpErrorResponse): void {
    console.error(loginError);
  }

  private handleGetUserResponse(userDataResponse: UserDataResponse): void {
    this.userService.setLoggedInUser(userDataResponse);
  }

  private handleGetUserError(loginError: HttpErrorResponse): void {
    console.error(loginError);
  }

  private initForm(): void {
    this.loginFormGroup = new FormGroup({
      userName: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required)
    });
  }

}
