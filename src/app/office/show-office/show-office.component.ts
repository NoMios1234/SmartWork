import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-show-office',
  templateUrl: './show-office.component.html',
  styleUrls: ['./show-office.component.css']
})
export class ShowOfficeComponent implements OnInit {

  constructor(private service:SharedService) { }

  officeList:any[];
  modalTitle:string;
  activateAddEditOffice:boolean=false;
  office:any;
  officePathImage=this.service.PhotoUrl + '/Office/';

  ngOnInit(): void {
    this.refreshOfficetList();
  }
  
  addClick(){
    this.office={
      Id:0,
      OfficeName:"",
      OfficeAddress:"",
      OfficePhoneNumber:"",
      OfficeDescription:"",
      PhotoFileName:"default_office_image.png"     
    }
    this.modalTitle="Add Office";
    this.activateAddEditOffice=true;
  }

  editClick(item: any){
    console.log(item);
    this.office=item;
    this.modalTitle="Edit Office";
    this.activateAddEditOffice=true;
  }

  deleteClick(item: any){
    if(confirm('Are you sure?')){
      console.log(item);
      this.service.deleteOffice(item.Id).subscribe(data =>{
        alert(data.toString());
        this.refreshOfficetList();
      })
    }
  }

  closeClick(){
    this.activateAddEditOffice=false;
    this.refreshOfficetList();
  }

  refreshOfficetList(){ 
    this.service.getOfficeList().subscribe(data=>{
      data.forEach(element => {
        element.PhotoFileFullPath = this.officePathImage + element.PhotoFileName;
      });
      this.officeList = data;
    });
  }
}
