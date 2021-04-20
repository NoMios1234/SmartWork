import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-show-company',
  templateUrl: './show-company.component.html',
  styleUrls: ['./show-company.component.css']
})
export class ShowCompanyComponent implements OnInit {

  constructor(private service:SharedService) { }

  CompanyList:any[];
  ModalTitle:string;
  ActivateAddEditCompanyComp:boolean=false;
  company:any;

  ngOnInit(): void {
    this.refreshCompanytList();
  }

  addClick(){
    this.company={
      id:0,
      companyName:"",
      companyAddress:"",
      companyPhoneNumber:"",
      companyDescription:"",
      photoFileName:"default_company_image.png"
    }
    this.ModalTitle = "Add new company";
    this.ActivateAddEditCompanyComp = true;
  }
  editClick(item: any){
    this.company=item;
    this.ModalTitle = "Edit company";
    this.ActivateAddEditCompanyComp = true;
  }
  closeClick(){
    this.ActivateAddEditCompanyComp = false;
    this.refreshCompanytList();
  }
  deleteClick(item: any){
    if(confirm("Are you shure?")){
      this.service.deleteCompany(item.id).subscribe(data=>{
        alert(data);
        this.refreshCompanytList();
      });
    }
  }
  refreshCompanytList(){  
    this.service.getCompanyList().subscribe(data=>{
      this.CompanyList = data;
    });
  }
}
