import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CaffImage } from 'src/app/models/caff-image';
import { HttpService } from 'src/app/services/http.service';

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.css']
})
export class FilesComponent implements OnInit {

  constructor(
    private httpService: HttpService, private http: HttpClient) { }

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
  
  fileDataForm = new FormGroup({
    comment: new FormControl(''),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required])
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
          
  get f(){
    return this.fileDataForm.controls;
  }
     
  onFileChange(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.fileDataForm.patchValue({
        fileSource: file
      });
    }
  }
     
  fileSubmit(){
    const formData = new FormData();
    formData.append('caffFile', this.fileDataForm.get('fileSource').value, this.fileDataForm.get('fileSource').value.name);
    let fileData = new CaffImage();
    fileData.comments = [];
    if (this.fileDataForm.get('comment').value !== '') {
      fileData.comments.push(this.fileDataForm.get('comment').value)
    }
    fileData.name = this.fileDataForm.get('fileSource').value.name;
    this.httpService.postCaffFileData(fileData).subscribe(response => {
      this.httpService.postCaffFile(formData, response.id).subscribe();
      this.getFiles();
    })
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
