import {ChangeDetectorRef, Component, Inject, OnInit} from '@angular/core';
import {CaffImage} from '../../models/caff-image';
import {HttpService} from '../../services/http.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute} from '@angular/router';
import {HttpErrorResponse} from '@angular/common/http';
import {UserService} from '../../services/user.service';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {

  public isUserAdmin = false;

  public showImage = false;
  public imageSource: string;
  public caffImagePreview: CaffImage;

  public addCommentFormGroup: FormGroup = new FormGroup({
    text: new FormControl(null, Validators.required)
  });

  private routedImageId: string;
  private readonly baseUrl: string;

  constructor(
    @Inject('BASE_URL') baseUrl: string,
    private activatedRoute: ActivatedRoute,
    private changeDetector: ChangeDetectorRef,
    private userService: UserService,
    private httpService: HttpService) {
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.isUserAdmin = this.userService.isAdmin();
    this.routedImageId = this.activatedRoute.snapshot.paramMap.get('imageid');
    this.loadImage();
  }

  public addComment(): void {
    if (this.addCommentFormGroup.valid) {
      const newComment = this.addCommentFormGroup.getRawValue();
      this.addCommentFormGroup.get('text').setValue(null);
      this.httpService.addComment(this.caffImagePreview.id, newComment)
        .subscribe(
          () => this.loadImage(),
          error => console.error(error)
        );
    }
  }

  private loadImage(): void {
    this.showImage = false;
    this.changeDetector.detectChanges();
    this.httpService.getImageById(this.routedImageId).subscribe(
      image => this.handleImageLoaded(image),
      error => this.handleImageError(error)
    );
  }

  private handleImageLoaded(image: CaffImage): void {
    this.caffImagePreview = image;
    this.imageSource = this.baseUrl + image.name;
    this.showImage = true;
    this.changeDetector.detectChanges();
  }

  private handleImageError(error: HttpErrorResponse): void {
    this.showImage = false;
  }

  private mockImage(): void {
    this.imageSource = 'https://lunawood.com/wp-content/uploads/2018/02/placeholder-image.png';
    this.caffImagePreview = {
      creationDate: '2020-11-28T13:55:05.055Z',
      id: '5fc256b9b8749f93dc0d7f56',
      name: 'kacsa.caff',
      comments: [
        {
          id: '5fc256b9b8749f93dc0d7f57',
          creationDate: '2020-11-28T13:55:05.055Z',
          text: 'A kacsak nagyon fura allatok!'
        },
        {
          id: '5fc256b9b8749f93dc0d7f58',
          creationDate: '2020-11-28T13:55:05.055Z',
          text: 'A kacsa pucsi haha!'
        },
      ]
    };
  }
}
