import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {HttpService} from '../../services/http.service';
import {RegisterData} from '../../models/register-data';
import {HttpErrorResponse} from '@angular/common/http';
import {RegisterResponse} from '../../models/register-response';
import {MessageService} from '../../services/message.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  public registerFormGroup: FormGroup;

  constructor(
    private messageService: MessageService,
    private httpService: HttpService) {
  }

  ngOnInit() {
    this.initForm();
  }

  public onSubmit(): void {
    if (this.registerFormGroup.valid) {
      const registerData: RegisterData = this.registerFormGroup.getRawValue();
      if (registerData.password === registerData.repeatedPassword) {
        this.httpService.register(registerData).subscribe(
          registerResponse => this.handleRegisterResponse(registerResponse),
          error => this.handleRegisterError(error),
        );
      } else {
        this.handleNotMatchingPasswords();
      }
    } else {
      this.registerFormGroup.markAllAsTouched();
    }
  }

  private handleRegisterResponse(registerResponse: RegisterResponse): void {

  }

  private handleRegisterError(error: HttpErrorResponse): void {

  }

  private handleNotMatchingPasswords(): void {
    this.messageService.showWarnMessage('The provided passwords are not the same', 'Failed password confirm');
  }

  private initForm(): void {
    this.registerFormGroup = new FormGroup({
      userName: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      repeatedPassword: new FormControl(null, Validators.required)
    });
  }

}
