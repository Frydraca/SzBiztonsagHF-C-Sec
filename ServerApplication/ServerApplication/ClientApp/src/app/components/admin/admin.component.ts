import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { HttpService } from '../../services/http.service';
import { MessageService } from '../../services/message.service';
import { User } from '../../models/user';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  users: User[];

  constructor(
    private adminService: AdminService,
    private httpService: HttpService,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.adminService.userListChanged.subscribe(
      () => (this.users = this.adminService.getAllUsers())
    );
    this.adminService.refreshList();
  }

  public onDeleteUser(id: string) {
    const user = this.adminService.getUserById(id);
    this.httpService.deleteUser(user).subscribe(
      result => this.handleDeleteResponse(result),
      error => this.handleDeleteError(error)
    );
  }

  private handleDeleteResponse(result: any) {
    this.messageService.showInfoMessage('Delete successfull');
    this.adminService.refreshList();
  }

  private handleDeleteError(error: any) {
    this.messageService.showErrorMessage('Delete failed');
  }
}
