import { Component, OnInit, Input } from '@angular/core';
import { SharedService } from 'src/app/shared/shared.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private service:SharedService) { }
  
  UserList:any[];
  ModalTitle:string;
  @Input() login:any;
  Id:string;
  Email:string;
  UserName:string;
  UserSurname:string;
  UserMiddleName:string; 
  PhoneNumber:string;
  Password:string;
  PasswordConfirm:string;

  ngOnInit(): void {
    
  }

  loadUserList(){
    this.Id=this.login.Id;
    this.Email=this.login.Email;
    this.UserName=this.login.UserName;
    this.UserSurname=this.login.UserSurname;
    this.UserMiddleName=this.login.UserMiddleName;
    this.PhoneNumber=this.login.PhoneNumber;
    this.Password=this.login.Password;
    this.PasswordConfirm=this.login.PasswordConfirm;
  }

  loginUser(){
    var val = {     
      Email:this.Email,              
      Password:this.Password     
    };
    console.log(val);
    this.service.loginUser(val).subscribe(res =>{
      console.log(res);
      this.service.getAuthorized().subscribe(res =>{
        console.log(res);
      });
    });
     
  }
}
