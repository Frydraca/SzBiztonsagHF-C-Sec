import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {LoginRegisterComponent} from './login-register/login-register.component';
import {AdminComponent} from './admin/admin.component';
import {DetailsComponent} from './details/details.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    FetchDataComponent,
    LoginRegisterComponent,
    AdminComponent,
    DetailsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent, pathMatch: 'full'},
      {path: 'fetch-data', component: FetchDataComponent},
      {path: 'login', component: LoginRegisterComponent},
      {path: 'details', component: DetailsComponent},
      {path: 'admin', component: AdminComponent},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {
}
