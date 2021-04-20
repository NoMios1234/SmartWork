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

  ngOnInit(): void {
    this.refreshCompanytList();
  }

  refreshCompanytList(){
    
    this.service.getCompanyList().subscribe(data=>{
      this.CompanyList = data;
      console.log(this.CompanyList);
    });
  }

}
