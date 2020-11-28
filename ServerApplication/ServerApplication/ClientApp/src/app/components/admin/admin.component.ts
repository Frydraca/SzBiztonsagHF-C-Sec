import { Component, OnInit } from "@angular/core";
import {HttpService} from '../../services/http.service';
import {User} from '../../models/user';

@Component({
  selector: "app-admin",
  templateUrl: "./admin.component.html",
  styleUrls: ["./admin.component.css"],
})
export class AdminComponent implements OnInit {
  users: User[];

  constructor(private httpService: HttpService) {}

  ngOnInit() {
    this.httpService.getAllUser().subscribe((users) => (this.users = users));
  }
}
