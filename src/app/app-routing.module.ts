import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';

import { CompanyComponent } from './company/company.component';
import { HomeComponent } from './home/home.component';
import { OfficeComponent } from './office/office.component';
import { LoginComponent } from './user/login/login.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';


const routes: Routes = [
  { path:'',redirectTo:'/user/registration', pathMatch:'full'},
  { path:'company', component:CompanyComponent },
  { path:'office', component:OfficeComponent },
  { path:'home', component:HomeComponent },
  {
    path: 'user', component: UserComponent,
    children: [
      { path: 'registration', component: RegistrationComponent },
      { path: 'login', component: LoginComponent }
    ]
  },
  {path:'home',component:HomeComponent,canActivate:[AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
