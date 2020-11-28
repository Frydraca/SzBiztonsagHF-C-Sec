import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  public routeParam: string;
  public usernameFormGroup: FormGroup;
  public passwordFormGroup: FormGroup;

  constructor(private route: ActivatedRoute) {}

  ngOnInit() {
    this.route.paramMap.subscribe(paramMap => {
      console.log(paramMap.get('id'));
      this.routeParam = paramMap.get('id');
    });
    this.initForms();
  }

  public onUpdateUsername(): void {
    const updatedName = this.usernameFormGroup.getRawValue();
    console.log(updatedName);
  }

  public onUpdatePassword(): void {
    const passwordFormValues = this.passwordFormGroup.getRawValue();
    console.log(passwordFormValues);
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
}
