import {Component, OnInit} from '@angular/core';
import {CaffImage} from '../../models/caff-image';
import {HttpService} from '../../services/http.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  public imageSource: string;
  public caffImagePreview: CaffImage;

  constructor(private httpService: HttpService) {
  }

  ngOnInit() {
    this.imageSource = 'https://lunawood.com/wp-content/uploads/2018/02/placeholder-image.png';
    this.caffImagePreview = {
      creationDate: '2020-11-28T13:55:05.055Z',
      id: '5fc256b9b8749f93dc0d7f56',
      name: 'kacsa.caff',
      comments: []
    };
  }


}
