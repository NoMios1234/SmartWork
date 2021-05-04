import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CompanyComponent } from './company/company.component';
import { OfficeComponent } from './office/office.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { UserComponent } from './user/user.component';


const routes: Routes = [
  { path:'',redirectTo:'/user/registration', pathMatch:'full'},
  { path:'company', component:CompanyComponent },
  { path:'office', component:OfficeComponent },
  { path:'user', component:UserComponent },
  { path:'user/registration', component:RegistrationComponent }
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
