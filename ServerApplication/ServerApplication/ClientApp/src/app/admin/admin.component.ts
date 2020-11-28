import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { User } from '../models/user';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  users: User[];

  constructor(private httpService: HttpService) {}

  ngOnInit() {
    this.httpService.getAllUser().subscribe(
      users => (this.users = users.filter(user => user.userName != 'admin')),
      error => console.log(error)
    );
  }

  public onDeleteUser(id: string) {
    console.log(id);
  }
}
