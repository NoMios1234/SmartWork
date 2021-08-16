import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-show-company',
  templateUrl: './show-company.component.html',
  styleUrls: ['./show-company.component.css']
})
export class ShowCompanyComponent implements OnInit {

  constructor(private service:SharedService) { }

  companyList:any[];
  modalTitle:string;
  activateAddEditCompany:boolean=false;
  company:any;
  companyPathImage=this.service.PhotoUrl + '/Company/';

  ngOnInit(): void {
    this.refreshCompanytList();
  }

  addClick(){
    this.company={
      Id:0,
      CompanyName:"",
      CompanyAddress:"",
      CompanyPhoneNumber:"",
      CompanyDescription:"",
      PhotoFileName:"default_company_image.png"     
    }
    this.modalTitle="Add Company";
    this.activateAddEditCompany=true;
  }

  editClick(item: any){
    console.log(item);
    this.company = item;
    this.modalTitle="Edit Company";
    this.activateAddEditCompany=true;
  }

  deleteClick(item: any){
    if(confirm('Are you sure?')){
      console.log(item);
      this.service.deleteCompany(item.Id).subscribe(data =>{
        alert(data.toString());
        this.refreshCompanytList();
      })
    }
  }

  closeClick(){
    this.activateAddEditCompany=false;
    this.refreshCompanytList();
  }

  refreshCompanytList(){ 
    this.service.getCompanyList().subscribe(data=>{
      data.forEach(element => {
        element.PhotoFileFullPath = this.companyPathImage + element.PhotoFileName;
      });
      this.companyList = data;
    });
  }
}
