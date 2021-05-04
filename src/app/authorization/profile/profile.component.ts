import { Component, OnInit } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  constructor(private service:SharedService) { }

  ModalTitle:string = "Profile";

  ngOnInit(): void {
  }

  profileClick(){
    console.log(this.service.getAuthorized());

  }
  logoutClick(){
    this.service.logoutUser().subscribe(res=>{
      console.log(res);
      alert("Logout");
      //this.service.isAuthorized = this.service.getAuthorized();
      console.log(this.service.getAuthorized());
    })
  }
  closeClick(){

  }

}
