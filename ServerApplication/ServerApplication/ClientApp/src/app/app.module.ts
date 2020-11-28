import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {LoginRegisterComponent} from './components/login-register/login-register.component';
import {UserLoginComponent} from './components/login-register/user-login/user-login.component';
import {UserRegisterComponent} from './components/login-register/user-register/user-register.component';
import {UserService} from './services/user.service';
import {HttpService} from './services/http.service';
import {AuthGuard} from './guards/auth.guard';
import {ToastrModule} from 'ngx-toastr';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {NavMenuComponent} from './components/nav-menu/nav-menu.component';
import {HomeComponent} from './components/home/home.component';
import {FetchDataComponent} from './components/fetch-data/fetch-data.component';
import {AdminComponent} from './components/admin/admin.component';
import {DetailsComponent} from './components/details/details.component';
import {UserDetailsComponent} from './components/user-details/user-details.component';

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
      {path: 'user-details/:id', component: UserDetailsComponent},
      {path: 'user-details', component: UserDetailsComponent},
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
