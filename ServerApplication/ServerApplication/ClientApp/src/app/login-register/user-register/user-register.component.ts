import {Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {HttpService} from '../../http.service';

@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  public registerFormGroup: FormGroup;

  constructor(private httpService: HttpService) {
  }

  ngOnInit() {
    this.initForm();
  }

  public onSubmit(): void {
    const registerValues = this.registerFormGroup.getRawValue();
    console.log(registerValues);
  }

  private initForm(): void {
    this.registerFormGroup = new FormGroup({
      userName: new FormControl(null, Validators.required),
      password: new FormControl(null, Validators.required),
      repeatedPassword: new FormControl(null, Validators.required)
    });
  }

}
