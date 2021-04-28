import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CompanyComponent } from './company/company.component';
import { ShowCompanyComponent } from './company/show-company/show-company.component';
import { AddEditCompanyComponent } from './company/add-edit-company/add-edit-company.component';
import { OfficeComponent } from './office/office.component';
import { ShowOfficeComponent } from './office/show-office/show-office.component';
import { AddEditOfficeComponent } from './office/add-edit-office/add-edit-office.component';
import { SharedService } from 'src/app/shared/shared.service';
import { AuthorizationComponent } from './authorization/authorization.component';
import { LoginComponent } from './authorization/login/login.component';
import { RegisterComponent } from './authorization/register/register.component';
import { ProfileComponent } from './authorization/profile/profile.component';
import { LoginMenuComponent } from './authorization/login-menu/login-menu.component';

@NgModule({
  declarations: [
    AppComponent,
    CompanyComponent,
    ShowCompanyComponent,
    AddEditCompanyComponent,
    OfficeComponent,
    ShowOfficeComponent,
    AddEditOfficeComponent,
    AuthorizationComponent,
    LoginComponent,
    RegisterComponent,
    ProfileComponent,
    LoginMenuComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [SharedService],
  bootstrap: [AppComponent]
})
export class AppModule { }
