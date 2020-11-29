import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.css']
})
export class FilesComponent implements OnInit {

  constructor(
    private httpService: HttpService) { }

    files = [];
    filesToShow = [];
    myFiles = [];
    isMyFile = false;
    myData;

  ngOnInit() {
    this.getFiles();
    this.getMyFiles();
    this.getMyData();
  }

  searchFiles = new FormGroup({
    name: new FormControl(),
    myfiles: new FormControl()
  });

  onSubmit() {
    console.log(this.searchFiles.get('name').value);
    console.log(this.searchFiles.get('myfiles').value);

    let searchString = this.searchFiles.get('name').value
    let onlyMyFiles = this.searchFiles.get('myfiles').value
    this.filesToShow = [];
    this.files.forEach(element => {
      if (onlyMyFiles) {
        if ((element.owner === this.myData.id) && (element.name.toLowerCase().includes(searchString.toLowerCase()))) {
          this.filesToShow.push(element);
        }
      } else {
        if (element.name.toLowerCase().includes(searchString.toLowerCase())) {
          this.filesToShow.push(element);
        }
      }
    });
  }

  getFiles() {
    this.httpService.listCaffFiles().subscribe(data => {
      this.files = data;
      this.filesToShow = data;
      console.log(data);
    })
  }

  getMyFiles() {
    this.httpService.listMyCaffFiles().subscribe(data => {
      this.myFiles = data;
      console.log(data);
    })
  }

  getMyData() {
    this.httpService.getUserData().subscribe(data => {
      this.myData = data;
      console.log(data);
    })
  }

  deleteCaffFile(id: string) {
    this.httpService.deleteCaffFile(id).subscribe(data => {
      this.getFiles();
    })
  }

}
