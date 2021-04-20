import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CompanyComponent } from './company/company.component';
import { OfficeComponent } from './office/office.component';

const routes: Routes = [
  { path:'company', component:CompanyComponent },
  { path:'office', component:OfficeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
