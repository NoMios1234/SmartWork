import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-add-edit-company',
  templateUrl: './add-edit-company.component.html',
  styleUrls: ['./add-edit-company.component.css']
})
export class AddEditCompanyComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() company:any;
  id:number;
  companyName:string;
  companyAddress:string;
  companyPhoneNumber:string;
  companyDescription:string;
  photoFileName:string;
  photoFilePath:string;

  ngOnInit(): void {
    this.id = this.company.id;
    this.companyName = this.company.companyName;
    this.companyAddress = this.company.companyAddress;
    this.companyPhoneNumber = this.company.companyPhoneNumber;
    this.photoFileName = this.company.photoFileName;
    this.companyDescription = this.company.companyDescription;
  }
  addCompany(){
    var val = {
      id:this.id,
      companyName:this.companyName,
      companyAddress:this.companyAddress,
      companyPhoneNumber:this.companyPhoneNumber,
      photoFileName:this.company.photoFileName,
      companyDescription:this.companyDescription
    }
    this.service.addCompany(val).subscribe(res =>{
      alert("Added successfuly");
    });
  }
  updateCompany(){
    var val = {
      id:this.id,
      companyName:this.companyName,
      companyAddress:this.companyAddress,
      companyPhoneNumber:this.companyPhoneNumber,
      photoFileName:this.company.photoFileName,
      companyDescription:this.companyDescription
    }
    this.service.updateCompany(val).subscribe(res =>{
      alert("Updated successfuly");
    });
  }

  uploadPhoto(event: any){
    var file = event.target.files[0];
    const formData:FormData=new FormData();
    formData.append('uploadFile',file,file.name);
    alert(this.photoFileName);
    this.service.uploadCompanyPhoto(formData).subscribe(data =>{
      this.photoFileName = data.toString();
      alert(this.photoFileName);
      this.photoFilePath = this.service.PhotoUrl+"/Company/"+this.photoFileName;
    });
  }
}
