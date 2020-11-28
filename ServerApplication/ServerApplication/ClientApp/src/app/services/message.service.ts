import { Injectable } from '@angular/core';
import {ToastrService} from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(private toastrService: ToastrService) { }

  public showInfoMessage(message: string, title?: string): void {
    this.toastrService.info(message, title);
  }

  public showWarnMessage(message: string, title?: string): void {
    this.toastrService.warning(message, title);
  }

  public showErrorMessage(message: string, title?: string): void {
    this.toastrService.error(message, title);
  }
}
