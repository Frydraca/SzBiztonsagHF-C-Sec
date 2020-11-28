import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {LoginRegisterComponent} from './login-register/login-register.component';
import {AdminComponent} from './admin/admin.component';
import {DetailsComponent} from './details/details.component';
import {UserLoginComponent} from './login-register/user-login/user-login.component';
import {UserRegisterComponent} from './login-register/user-register/user-register.component';
import {UserService} from './services/user.service';
import {HttpService} from './services/http.service';
import {UserDetailsComponent} from './user-details/user-details.component';
import {AuthGuard} from './guards/auth.guard';
import {ToastrModule} from 'ngx-toastr';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    LoginRegisterComponent,
    AdminComponent,
    DetailsComponent,
    UserLoginComponent,
    UserRegisterComponent,
    UserDetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'fetch-data', component: FetchDataComponent},
      {path: 'login', component: LoginRegisterComponent},
      {path: 'details', component: DetailsComponent, canActivate: [AuthGuard]},
      {path: 'admin', component: AdminComponent, canActivate: [AuthGuard]},
    ]),
    ReactiveFormsModule,
    ToastrModule.forRoot({
      timeOut: 5000,
      positionClass: 'toast-bottom-right'
    })
  ],
  providers: [
    HttpService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
